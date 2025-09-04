using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RecipeHubAPI;
using RecipeHubAPI.Database;
using RecipeHubAPI.Repository.Implementations;
using RecipeHubAPI.Repository.Interface;
using RecipeHubAPI.Services;
using RecipeHubAPI.Services.Auth;
using RecipeHubAPI.Services.Implementation;
using RecipeHubAPI.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

string secretKey = builder.Configuration.GetValue<string>("AppSettings:SecretKey") ?? throw new Exception("Unable to access key to connect to the database.");
string ApiUrl = builder.Configuration.GetValue<string>("AppSettings:ApiUrl") ?? throw new Exception("Unable to feth Api Url value");
string clientUrl = builder.Configuration.GetValue<string>("AppSettings:clientUrl") ?? throw new Exception("Unable to access client url variable");


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
builder.Services.AddScoped<IGroupRepository, GroupRepository>();
builder.Services.AddScoped<IRecipeRepository, RecipeRepository>();
builder.Services.AddScoped<IRecipeIngredientsRepository, RecipeIngredientsRepository>();
builder.Services.AddScoped<IStepsRepository, StepsRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IMeasurementUnitRepository, MeasurementUnitRepository>();
builder.Services.AddScoped<IRecipeService, RecipeService>();

// Optional but removes legacy claim mapping surprises
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

var TokenValidationParameters = new TokenValidationParameters
{
    ValidIssuer = ApiUrl,
    ValidAudience = ApiUrl,
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
    ClockSkew = TimeSpan.Zero,
    NameClaimType = "userId",
    RoleClaimType = ClaimTypes.Role,
};

builder.Services.AddAuthentication(
    options =>
    {
        // Technically not necessary to add default schema as there is only 1
        // authentication service registered.
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    }
    )
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = TokenValidationParameters;
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("User", policy => policy.RequireClaim(ClaimTypes.Role, "User"));
    options.AddPolicy("Admin", policy => policy.RequireClaim(ClaimTypes.Role, "Admin"));
    // Policy not used for now. Used for checking if the User is the same as the one in the route.
    //options.AddPolicy("SameUserOrAdmin", policy => policy.RequireAuthenticatedUser().AddRequirements(new SameUserRequirement()));
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<IAuthorizationHandler, SameUserHandler>();
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
    await next(); // Process the request first

    // Ensure the response has NOT started before modifying it
    if (context.Response.StatusCode == StatusCodes.Status404NotFound && !context.Response.HasStarted)
    {
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync("{\"message\": \"Resource not found.\"}");
    }
});


app.MapControllers();

app.Run();
