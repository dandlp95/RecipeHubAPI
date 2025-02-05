using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using RecipeHubAPI.Database;

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

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
