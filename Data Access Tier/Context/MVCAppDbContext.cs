using Data_Access_Tier.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data_Access_Tier.Context
{
    public class MVCAppDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public MVCAppDbContext(DbContextOptions<MVCAppDbContext> options) : base(options)
        {
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}
