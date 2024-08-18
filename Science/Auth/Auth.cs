using System.Security.Cryptography;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Science.Config;

namespace Science.Auth;

public static class Auth
{
    private static RsaSecurityKey GetSecurityKey(string publicKey)
    {
        var rsa = RSA.Create();
        rsa.ImportFromPem(publicKey);
        return new RsaSecurityKey(rsa);
    }

    public static void AddMosPolytechAuth(this IServiceCollection services)
    {
        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = GetSecurityKey(Cfg.RsaPublicKey),
                    ValidIssuer = "humanresourcesdepartmentapi.mospolytech.ru",
                    ValidAudience = "HumanResourcesDepartment",
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                }
            );

        services.AddAuthorization();

        services.AddSwaggerGen(swagger =>
        {
            swagger.SwaggerDoc("v1", new OpenApiInfo { Title = "Science", Version = "v1" });

            swagger.AddSecurityDefinition(
                "Bearer",
                new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description =
                        "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                }
            );

            swagger.AddSecurityRequirement(
                new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer",
                            },
                        },
                        Array.Empty<string>()
                    },
                }
            );
        });
    }

    public static void UseMosPolytechAuth(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Science API V1");
        });

        app.UseAuthentication();
        app.UseAuthorization();
    }
}
