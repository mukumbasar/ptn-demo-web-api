using Castle.Core.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Pomelo.EntityFrameworkCore.MySql;
using MySqlConnector;
using PtnDemoProject.DAL.Context;
using PtnDemoProject.DAL.DataSeed;
using PtnDemoProject.DAL.Repositories.Abstract;
using PtnDemoProject.DAL.Repositories.Concrete;
using PtnDemoProjectAPI.Entities.Concrete.DbSets;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace PtnDemoProject.DAL.Extensions
{
    public static class DALDependenyInjectionConfigurator
    {
        /// <summary>
        /// Configures DbContext and adds it to the collection.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="configuration">The configuration which stores DbContext settings.</param>
        /// <returns>The updated service collection.</returns>
        public static IServiceCollection AddDbContextConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("AzureDbConnectionString");

            var serverVersion = ServerVersion.AutoDetect(connectionString);

            services.AddDbContext<PtnDemoProjectDbContext>(options =>
            {
                options.UseMySql(connectionString, serverVersion);
            });

            // Configure Identity
            services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.SignIn.RequireConfirmedEmail = false;
            })
            .AddEntityFrameworkStores<PtnDemoProjectDbContext>()
            .AddDefaultTokenProviders();

            return services;
        }

        /// <summary>
        /// Configures MongoDb and adds it to the collection.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="configuration">The configuration which stores MongoDb settings.</param>
        /// <returns>The updated service collection.</returns>
        public static IServiceCollection AddMongoDbConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IMongoClient>(sp =>
        new MongoClient(configuration.GetConnectionString("MongoDbConnectionString")));

            var mongoDbName = configuration.GetValue<string>("MongoSettings:MongoDbDatabaseName");

            services.AddScoped<IMongoDatabase>(sp =>
                sp.GetRequiredService<IMongoClient>().GetDatabase(mongoDbName));

            return services;
        }

        /// <summary>
        /// Add the repositories to the service collection.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <returns>The updated service collection.</returns>
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IBuildingTypeRepository, BuildingTypeRepository>();
            services.AddScoped<IBuildingRepository, BuildingRepository>();

            return services;
        }

        /// <summary>
        /// Applies data seeding.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="configuration">The configuration which stores database settings.</param>
        /// <returns>The updated service collection.</returns>
        public static IServiceCollection SeedDatas(this  IServiceCollection services) 
        {
            var serviceProvider = services.BuildServiceProvider();

            DataSeed.DataSeed.SeedAsync(serviceProvider).GetAwaiter().GetResult();

            return services;
        }
    }
}
