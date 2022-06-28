using KCommerceAPI.DataAccess.EfCore;
using KCommerceAPI.Logic.Person.Employee;
using KCommerceAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var configure = builder.Configuration;
var environment = builder.Environment;
var posgresCnnStr = configure.GetValue<string>("DB:CnnStr");
var tokenRelatedSettings = new TokenRelatedSettings();

configure.GetSection("Token").Bind(tokenRelatedSettings);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//configuration

if (environment.EnvironmentName == "Development" || environment.EnvironmentName == "Test")
{
    builder.Services.AddDbContext<KComDbContext>(options =>
                    options.UseNpgsql(posgresCnnStr).EnableSensitiveDataLogging(true));
}
else
{
    // for production
    builder.Services.AddDbContext<KComDbContext>(options =>
                    options.UseNpgsql(posgresCnnStr));
}

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = tokenRelatedSettings.Issuer,
                            ValidAudience = tokenRelatedSettings.Audience,

                            IssuerSigningKey = new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(tokenRelatedSettings.SecurityKey)),
                            ClockSkew = System.TimeSpan.Zero
                        };

                        options.Events = new JwtBearerEvents
                        {
                            OnAuthenticationFailed = context =>
                            {
                                if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                                {
                                    context.Response.Headers.Add("Token-Expired", "true");
                                }
                                return System.Threading.Tasks.Task.CompletedTask;
                            }
                        };
                    }
            );

var services = builder.Services;

services.AddScoped<IEmployeeLoginLogic, EmployeeLoginLogic>();
services.AddScoped<IEmployeeLogic, EmployeeLogic>();


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
