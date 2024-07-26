using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PtnDemoProjectAPI.BLL.Configurations;
using PtnDemoProjectAPI.BLL.HelperServices;
using PtnDemoProjectAPI.BLL.Mapping;
using PtnDemoProjectAPI.BLL.Services.Abstact;
using PtnDemoProjectAPI.BLL.Services.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PtnDemoProjectAPI.BLL.Extensions
{
    public static class BLLDependencyInjectionConfigurator
    {
        /// <summary>
        /// Adds jwt configs.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="configuration">The configuration which stores jwt settings.</param>
        /// <returns>The updated service collection.</returns>
        public static IServiceCollection AddJwtConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            // Make JWTOption available throughout the application via dependency injection.
            services.Configure<TokenOptions>(configuration.GetSection("TokenOptions"));
            // Retrieve JWTOption to be used.
            var tokenOptions = configuration.GetSection("TokenOptions").Get<TokenOptions>();
            // Set up authentication schemes.
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            })
                // Configure JWT Bearer authentication.
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
                {
                    opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                    {
                        ValidIssuer = tokenOptions.Issuer,
                        ValidAudience = tokenOptions.Audience[0],
                        ValidateIssuerSigningKey = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        IssuerSigningKey = JwtService.GetSymmetricSecurityKey(tokenOptions.SecurityKey),
                        ClockSkew = TimeSpan.Zero
                    };
                });

            return services;
        }

        /// <summary>
        /// Adds AutoMapper with mapping profiles.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <returns>The updated service collection.</returns>
        public static IServiceCollection AddOtherBLLExtensions(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(BLLBuildingMappingProfile).Assembly);

            return services;
        }

        /// <summary>
        /// Adds services to the collection.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <returns>The updated service collection.</returns>
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IBuildingService, BuildingService>();
            services.AddScoped<IBuildingTypeService, BuildingTypeService>();
            services.AddScoped<IAccountService, AccountService>();
            return services;
        }
    }
}
