using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Unchase.Swashbuckle.AspNetCore.Extensions.Extensions;

namespace AvitoBackendDriven.API.Services;

/// <summary>
/// Сервис конфигурации Swagger
/// </summary>
public static class SwaggerService
{
    /// <summary>
    /// Добавить сервис конфигурации Swagger
    /// </summary>
    /// <param name="services">Service collection</param>
    public static IServiceCollection AddSwaggerService(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
        });

        services.AddVersionedApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });

        services.AddSwaggerGen(config =>
        {
            config.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "AvitoBackendDriven.API",
                Description = "Документация по использованию AvitoBackendDriven.API.\n" +
                    "Описание всех объектов находится в самом низу страницы"
            });

            var subProjectAssemblies = AppDomain.CurrentDomain
                .GetAssemblies()
                .Where(u => u.FullName!.Contains("AvitoBackendDriven")).ToArray();

            foreach (var subProject in subProjectAssemblies)
            {
                var xmlFile = $"{subProject.GetName().Name}.xml";
                var assemblyRootPath = Directory.GetParent(subProject.Location)!.FullName;
                var xmlPath = Path.Combine(assemblyRootPath, xmlFile);
                
                if (File.Exists(xmlPath))
                {
                    config.IncludeXmlComments(xmlPath);
                }
            }

            config.IncludeXmlCommentsFromInheritDocs(includeRemarks: true, excludedTypes: typeof(string));
            config.AddEnumsWithValuesFixFilters();

            // config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            // {
            //     In = ParameterLocation.Header,
            //     Description = "Поместите токен доступа в поле ниже",
            //     Name = "Authorization",
            //     Type = SecuritySchemeType.Http,
            //     Scheme = "Bearer",
            //     BearerFormat = "JWT"
            // });
            //
            // config.AddSecurityRequirement(new OpenApiSecurityRequirement
            // {
            //     {
            //         new OpenApiSecurityScheme
            //         {
            //             Reference = new OpenApiReference
            //             {
            //                 Type = ReferenceType.SecurityScheme,
            //                 Id = "Bearer"
            //             }
            //         },
            //         Array.Empty<string>()
            //     }
            // });
        });

        return services;
    }
}