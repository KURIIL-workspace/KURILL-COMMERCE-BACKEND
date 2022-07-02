using KCommerceAPI.Common;
using KCommerceAPI.DataAccess.EfCore;
using KCommerceAPI.Logic.Person.Employee;
using KCommerceAPI.Logic.Person.Supplier;
using KCommerceAPI.Logic.Purchase.PurchaseInvoice;
using KCommerceAPI.Logic.Purchase.PurchaseOrder;
using KCommerceAPI.Mappers;
using KCommerceAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

string API_NAME = "SMR Production API";

string CORS_POLICY_NAME = "cors_policy";

// Add services to the container.

builder.Services.AddControllers();

var configure = builder.Configuration;
var environment = builder.Environment;
var posgresCnnStr = configure.GetValue<string>("DB:CnnStr");
var tokenRelatedSettings = new TokenRelatedSettings();

configure.GetSection("Token").Bind(tokenRelatedSettings);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

string[] GetAllowedOrigins()
{
    var origins = configure.GetSection("Api:AllowedOrigins").Value;

    if (!string.IsNullOrEmpty(origins))
    {
        var splittedOrigins = origins.Split(",");
        return splittedOrigins;
    }

    return null;
}
var allowedOrigins = GetAllowedOrigins();

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

builder.Services.AddCors(options =>
{
    options.AddPolicy(CORS_POLICY_NAME,
    builder =>
    {
        builder.WithOrigins(allowedOrigins)
                .AllowAnyHeader()
                .AllowAnyMethod();
    });
});

builder.Services.AddSwaggerGen(c =>
{
    /*c.SwaggerDoc(
        new OpenApiInfo
        {
            Title = API_NAME,
            //Version = apiVersion,
            Description = API_NAME
        }
    );*/

    /*
    swagger version 2 does not support same url with different query parameters.
    so including of the following line is required to show the swagger doc without
    any errors.       
    */
    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

    //var filePath = Path.Combine(System.AppContext.BaseDirectory, "api.xml");
    //c.IncludeXmlComments(filePath);

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = System.IO.Path.Combine(AppContext.BaseDirectory, xmlFile);
    //c.IncludeXmlComments(xmlPath);

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}

                    }
                });
});

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
                    });

var services = builder.Services;

services.AddScoped<UserAuthenticationHandler>(u => new UserAuthenticationHandler(
                                                             tokenRelatedSettings.SecurityKey,
                                                             tokenRelatedSettings.AccessTokenExpiresMin,
                                                             tokenRelatedSettings.Issuer,
                                                             tokenRelatedSettings.Audience,
                                                             tokenRelatedSettings.NoOfDaysRefreshTokenValid));

//service mapping
services.AddScoped<IEmployeeLoginLogic, EmployeeLoginLogic>();
services.AddScoped<IEmployeeLogic, EmployeeLogic>();
services.AddScoped<IPurchaseOrderLogic, PurchaseOrderLogic>();
services.AddScoped<ISupplierLogic, SupplierLogic>();
services.AddScoped<IPurchaseInvoiceLogic, PurchaseInvoiceLogic>();

services.AddAutoMapper(typeof(PersonRelatedMapper));
services.AddAutoMapper(typeof(ContactRelatedMapper));
services.AddAutoMapper(typeof(PurchaseRelatedMapper));



// Configure the HTTP request pipeline.
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseCors(CORS_POLICY_NAME);

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
