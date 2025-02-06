using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RecipeHubAPI;
using RecipeHubAPI.Database;
using RecipeHubAPI.Repository.Implementations;
using RecipeHubAPI.Repository.Interface;
using RecipeHubAPI.Services;
using RecipeHubAPI.Services.Implementation;
using RecipeHubAPI.Services.Interfaces;
using System.Text;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

string secretKey = builder.Configuration.GetValue<string>("AppSettings:SecretKey") ?? throw new Exception("Unable to access key to connect to the database.");
string ApiUrl = builder.Configuration.GetValue<string>("AppSettings:ApiUrl") ?? throw new Exception("Unable to feth Api Url value");
string clientUrl = builder.Configuration.GetValue<string>("AppSettings:clientUrl") ?? throw new Exception("Unable to access client url variable");

var TokenValidationParameters = new TokenValidationParameters
{
    ValidIssuer = ApiUrl,
    ValidAudience = ApiUrl,
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
    ClockSkew = TimeSpan.Zero
};

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins, 
                       policy => {
                           policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                       });
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQLConnection"));
});

builder.Services.AddAutoMapper(typeof(MappingConfig));
builder.Services.AddTransient<IExceptionHandler, ExceptionHandler>();
builder.Services.AddTransient<IPasswordService, PasswordService>();
builder.Services.AddTransient<ITokenService, TokenService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

//// Add services to the container.
builder.Services.AddAuthentication(
    options =>
    {
        // Technically not necessary to add default schema as there is only 1
        // authentication service registered.
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    }
    )
    .AddJwtBearer(cfg =>
    {
        cfg.TokenValidationParameters = TokenValidationParameters;
    });

builder.Services.AddAuthorization(cfg =>
{
    cfg.AddPolicy("User", policy => policy.RequireClaim("type", "User"));
    cfg.AddPolicy("Admin", policy => policy.RequireClaim("type", "Admin"));
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "RecipeHub", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter JWT with Bearer into field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
    { new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer"}
            },
        Array.Empty<string>()
    }
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseCors(MyAllowSpecificOrigins);
app.UseAuthorization();

app.Use(async (context, next) =>
{
    await next(); // Process the request and response

    // Check if the response status code is 404
    if (context.Response.StatusCode == StatusCodes.Status404NotFound)
    {
        // Set the content type to JSON
        context.Response.ContentType = "application/json";
        // Write a custom 404 response
        await context.Response.WriteAsync("{\"message\": \"Resource not found.\"}");
    }
});

app.MapControllers();

app.Run();
