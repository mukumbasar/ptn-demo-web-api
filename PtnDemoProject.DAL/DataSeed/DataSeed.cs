using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PtnDemoProject.DAL.Context;
using PtnDemoProject.DAL.Repositories.Abstract;
using PtnDemoProject.DAL.Repositories.Concrete;
using PtnDemoProjectAPI.Entities.Concrete.DbSets;
using PtnDemoProjectAPI.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PtnDemoProject.DAL.DataSeed
{
    internal static class DataSeed
    {
        private const string AdminEmail = "admin@ptndemoproject.com";
        private const string AdminPassword = "Admin-3";

        /// <summary>
        /// Seeds the database with initial data if it is not already present.
        /// </summary>
        /// <param name="serviceProvider">The service provider for required services.</param>
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            //Get DbContext 
            var context = serviceProvider.GetRequiredService<PtnDemoProjectDbContext>();

            // Get BuildingTypeRepository
            var buildingTypeRepository = serviceProvider.GetRequiredService<IBuildingTypeRepository>();

            if (!context.Roles.Any())
            {
                await AddRoles(context);
            }

            if (!context.Users.Any(user => user.Email == AdminEmail))
            {
                await AddAdmin(context);
            }

            var hasBuildingTypes = await buildingTypeRepository.AnyAsync();

            if (!hasBuildingTypes)
            {
                await AddBuildingTypes(buildingTypeRepository);
            }
        }

        /// <summary>
        /// Adds roles to the database if they do not already exist.
        /// </summary>
        /// <param name="context">The DbContext to interact with the database.</param>
        private static async Task AddRoles(PtnDemoProjectDbContext context)
        {
            string[] roles = Enum.GetNames(typeof(Roles));

            for (int i = 0; i < roles.Length; i++)
            {
                if (await context.Roles.AnyAsync(role => role.Name == roles[i]))
                {
                    continue;
                }

                await context.Roles.AddAsync(new AppRole { Id = Guid.NewGuid().ToString(), Name = roles[i], NormalizedName = roles[i].ToUpperInvariant() });
            }

            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Adds building types to the repository if they do not already exist.
        /// </summary>
        /// <param name="buildingTypeRepository">The repository to interact with building types.</param>
        private static async Task AddBuildingTypes(IBuildingTypeRepository buildingTypeRepository)
        {
            string[] buildingTypes = Enum.GetNames(typeof(BuildingTypes));

            for (int i = 0; i < buildingTypes.Length; i++)
            {
                if (await buildingTypeRepository.AnyAsync(bt => bt.Name == buildingTypes[i]))
                {
                    continue;
                }

                await buildingTypeRepository.CreateAsync(new BuildingType { Name = buildingTypes[i] });
            }
        }

        /// <summary>
        /// Adds an admin user to the database if not already present.
        /// </summary>
        /// <param name="context">The DbContext to interact with the database.</param>
        private static async Task AddAdmin(PtnDemoProjectDbContext context)
        {
            AppUser user = new()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = AdminEmail,
                NormalizedUserName = AdminEmail.ToUpper(),
                Email = AdminEmail,
                NormalizedEmail = AdminEmail.ToUpper(),
                EmailConfirmed = true
            };

            user.PasswordHash = new PasswordHasher<AppUser>().HashPassword(user, AdminPassword);
            var identityUser = await context.Users.AddAsync(user);

            var adminRoleId = context.Roles.FirstOrDefault(role => role.Name == Roles.Admin.ToString())!.Id;

            await context.UserRoles.AddAsync(new IdentityUserRole<string> { UserId = user.Id, RoleId = adminRoleId });

            await context.SaveChangesAsync();
        }
    }
}
