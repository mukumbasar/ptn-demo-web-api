using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PtnDemoProject.DAL.Context
{
    public class PtnDemoProjectDbContextFactory : IDesignTimeDbContextFactory<PtnDemoProjectDbContext>
    {
        //Design-time configurations
        public PtnDemoProjectDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<PtnDemoProjectDbContext>();
            var connectionString = configuration.GetConnectionString("AzureDbConnectionString");

            builder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

            return new PtnDemoProjectDbContext(builder.Options);
        }
    }
}
