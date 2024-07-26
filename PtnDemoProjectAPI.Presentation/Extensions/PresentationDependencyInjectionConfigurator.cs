using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;
using PtnDemoProjectAPI.BLL.Mapping;
using System.Reflection;

namespace PtnDemoProjectAPI.Presentation.Extensions
{
    public static class PresentationDependencyInjectionConfigurator
    {
        /// <summary>
        /// Adds FluentValidation with the validators.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <returns>The updated service collection.</returns>
        public static IServiceCollection AddFluentValidation(this IServiceCollection services)
        {
            services
            .AddFluentValidationAutoValidation(config => config.DisableDataAnnotationsValidation = true)
            .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            return services;
        }

        /// <summary>
        /// Configures SwaggerGen for token usage through the test ui.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <returns>The updated service collection.</returns>
        public static IServiceCollection AddSwaggerGenConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Ptn Demo Project API", Version = "v1" });

                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "JWT Authentication",
                    Description = "Enter your JWT token",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                };

                c.AddSecurityDefinition("Bearer", securityScheme);

                var securityRequirement = new OpenApiSecurityRequirement
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
        };

                c.AddSecurityRequirement(securityRequirement);
            });

            return services;
        }

        /// <summary>
        /// Enables cors.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <returns>The updated service collection.</returns>
        public static IServiceCollection AddCorsConfiguration(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigins",
                    policy =>
                    {
                        policy.WithOrigins("https://ptndemoapi.azurewebsites.net", "http://localhost:5173", "https://mukumbasar.github.io")
                              .AllowAnyHeader()
                              .AllowAnyMethod();
                    });
            });

            return services;
        }
    }
}
