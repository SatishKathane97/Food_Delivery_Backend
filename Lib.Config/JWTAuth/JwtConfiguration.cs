using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace Lib.Config.JWTAuth
{
    public static class JwtConfiguration
    {
        public static IServiceCollection AddJwtSecurity(this IServiceCollection services, IConfiguration config)
        {
            var opts = config.GetSection("AuthToken").Get<AuthToken>()
                       ?? throw new Exception("AuthToken config missing in appsettings.json");

            services.Configure<AuthToken>(config.GetSection("AuthToken"));

            var key = Encoding.UTF8.GetBytes(
                opts.Key ?? throw new Exception("JWT Key missing in configuration")
            );

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = opts.Issuer ?? throw new Exception("Issuer missing"),
                        ValidateAudience = true,
                        ValidAudience = opts.Audience ?? throw new Exception("Audience missing"),
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        RoleClaimType = ClaimTypes.Role,
                        ClockSkew = TimeSpan.Zero,
                        ValidateLifetime = true
                    };
                });

            return services;
        }

    }
}
