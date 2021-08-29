
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;

namespace Service.Core.Weather.Extensions;

public static class JwtSecurityExtensions
{
    public static void AddJwtSecurity(this IServiceCollection services,IConfiguration configuration)
    {
        var jwtBearerOptions = configuration
            .GetSection("IdentityServer")
            .Get<JwtBearerOptions>();
        Assembly asm = Assembly
            .GetExecutingAssembly();
        var controllers = asm.GetTypes()
            .Where(type => typeof(ControllerBase).IsAssignableFrom(type));
        var policies = controllers
            .SelectMany(c => c.GetCustomAttributes<AuthorizeAttribute>());
        var scopes = policies
            .Select(p => p.Policy);

        services.AddAuthentication("Bearer")
            .AddJwtBearer("Bearer", options =>
            {
                options.Authority = jwtBearerOptions.Authority;
                options.RequireHttpsMetadata = jwtBearerOptions.RequireHttpsMetadata;
                options.TokenValidationParameters = jwtBearerOptions.TokenValidationParameters;
            });

        services.AddAuthorization(options =>
        {
            foreach (var scope in scopes) options.AddPolicy(scope, 
                policy => policy.RequireClaim("scope", scope));
        });
    }

    public static void UseJwtAuthentication(this WebApplication app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
    }

}
