using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PtnDemoProjectAPI.Entities.Concrete.DbSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PtnDemoProject.DAL.Context
{
    public class PtnDemoProjectDbContext : IdentityDbContext<AppUser,  AppRole, string>
    {
        public PtnDemoProjectDbContext(DbContextOptions<PtnDemoProjectDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(IEntityTypeConfiguration<>).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
