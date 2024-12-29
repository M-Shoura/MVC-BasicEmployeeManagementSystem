using IKIA.DAL.Models.Departments;
using IKIA.DAL.Models.Employees;
using IKIA.DAL.Models.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace IKIA.DAL.Presistence.Data
{
    public class ApplicationDbcontext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }

        public ApplicationDbcontext(DbContextOptions<ApplicationDbcontext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);    

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
