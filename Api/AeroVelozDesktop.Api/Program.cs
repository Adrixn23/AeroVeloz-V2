using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using AeroVeloz.Infraestructure.Persistence.context;
using AeroVeloz.IOC.Dependencies;
using AeroVeloz.Infraestructure.Integrations.Notifications.Validation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AeroVelozContext>(options =>
    options
        .UseSqlServer(connectionString, sqlOptions =>
        {
            sqlOptions.EnableRetryOnFailure(
                maxRetryCount: 3,
                maxRetryDelay: TimeSpan.FromSeconds(10),
                errorNumbersToAdd: null);
        })
        .ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning)));

builder.Services.AddDomainServices();
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddNotificationServices(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var secretKey = builder.Configuration["JwtOptions:SecretKey"] ?? "AeroVelozSuperSecretKey_12345678901234567890";
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JwtOptions:Issuer"] ?? "AeroVelozApp",
            ValidAudience = builder.Configuration["JwtOptions:Audience"] ?? "AeroVelozDesktop",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
        };
    });
builder.Services.AddAuthorization();

var app = builder.Build();


var logger = app.Services.GetRequiredService<ILogger<Program>>();

logger.LogInformation("========================================");
logger.LogInformation("🚀 Iniciando AeroVeloz API");
logger.LogInformation("========================================");

SmtpConfigurationValidator.ValidateConfiguration(app.Configuration, logger);
OneSignalConfigurationValidator.ValidateConfiguration(app.Configuration, logger);

logger.LogInformation("========================================");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();