
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PosTech.Hackathon.Users.Api.Configuration;
using PosTech.Hackathon.Users.Application.Interfaces.Services;
using PosTech.Hackathon.Users.Application.Services;
using PosTech.Hackathon.Users.Domain.Entities;
using PosTech.Hackathon.Users.Infra.Context;
using PosTech.Hackathon.Users.Infra.Interfaces;
using PosTech.Hackathon.Users.Infra.Producers;


namespace PosTech.TechChallenge.Users.Api;

public class Startup(IConfiguration configuration)
{
    public IConfiguration Configuration { get; } = configuration;

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddDbContext<UserDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")),
            ServiceLifetime.Scoped
        );
        services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<UserDbContext>()
                .AddDefaultTokenProviders();
        services.AddScoped<IProducer, Producer>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddLogging();
        services.AddAuthentication(options => options.DefaultAuthenticateScheme =
                JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("c2d4a61141f0616bef9eac3c6cd539c454509dddfed9d0df54a6a17bfbe9172b")),
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ClockSkew = TimeSpan.Zero
                });
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddUserUseCases();
        services.AddAuthenticationUseCases();
    }

    public void Configure(IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseHttpsRedirection();
    }
}