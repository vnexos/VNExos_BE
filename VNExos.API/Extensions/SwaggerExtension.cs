using Microsoft.OpenApi.Models;

namespace VNExos.API.Extensions;

public static class SwaggerExtension
{
    public static void AddSwagger(this WebApplicationBuilder builder)
    {
        var excludeEndpoints = new List<string> { "api/users/login", "api/users/register", "api/languages" };
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "VNExos API", Version = "v2" });
            c.MapType<IFormFile>(() => new OpenApiSchema
            {
                Type = "string",
                Format = "binary"
            });
            c.EnableAnnotations();

            c.AddSecurityDefinition("token", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "token"}
                    },
                    new string[] { }
                }
            });
        });
    }
}
