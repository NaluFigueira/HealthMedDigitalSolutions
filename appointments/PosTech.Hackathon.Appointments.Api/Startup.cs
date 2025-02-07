using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

using PosTech.Hackathon.Appointments.Api.Configuration;
using PosTech.Hackathon.Appointments.Infra.Context;

namespace PosTech.Hackathon.Appointments.Api;

public class Startup(IConfiguration configuration)
{
    public IConfiguration _configuration { get; } = configuration;

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new JsonDateTimeConverter());
        });
        services.AddDbContext<AppointmentsDBContext>(options =>
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            options.UseSqlServer(connectionString);
        });

        services.AddAuthentication(options => options.DefaultAuthenticateScheme =
            JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("c2d4a61141f0616bef9eac3c6cd539c454509dddfed9d0df54a6a17bfbe9172b")),
                ValidateAudience = false,
                ValidateIssuer = false,
                ClockSkew = TimeSpan.FromMinutes(15),
            });

        services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme {
                        Reference = new OpenApiReference {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });

        services.AddLogging();
        services.AddEndpointsApiExplorer();
        services.AddAppointmentUseCases();
        services.RegisterConsumers();
        services.AddAppointmentInfraServices();
    }

    public void Configure(IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseHttpsRedirection();
    }
}