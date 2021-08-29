
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace Service.Core.Weather.Extensions;

public static class SwaggerExtentions
{
    public static void AddSwaggerAuthorizationHeader(this IServiceCollection services)
    {
        var jwtSecurityScheme = new OpenApiSecurityScheme
        {
            Scheme = "bearer",
            BearerFormat = "JWT",
            Name = "Authentication",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
            Reference = new OpenApiReference
            {
                Id = JwtBearerDefaults.AuthenticationScheme,
                Type = ReferenceType.SecurityScheme
            }
        };
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new() { Title = "Service.Core.Weather", Version = "v1" });
            c.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                { jwtSecurityScheme, Array.Empty<string>() }
            });
        });
    }

    public static void UseSwaggerBehindGateway(this WebApplication app)
    {
        app.UseSwagger(c =>
        {
            c.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
            {
                if (httpReq.Headers.TryGetValue("X-Forwarded-Path", out StringValues values) && values.Count > 0)
                {
                    swaggerDoc.Servers = new List<OpenApiServer>
                    {
                        new OpenApiServer {  Url = $"{httpReq.Scheme}://{httpReq.Host.Value}{values.FirstOrDefault()}" }
                    };
                }
            });
        });

        app.UseSwaggerUI(options => options.SwaggerEndpoint("v1/swagger.json", "Weather Api (v1)"));
    }
}
