using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebAPI_Project.Models
{
    public class ITIEntity:IdentityDbContext<ApplicationUser>//DbContext
    {
        public ITIEntity()
        {
            
        }
        public ITIEntity(DbContextOptions options):base(options)

        {
            
        }

        public DbSet<Employee> Employees{ get; set; }
        public DbSet<Department> Department{ get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
         
            optionsBuilder.UseSqlServer("Data Source=.; Initial Catalog=WebAPI; Integrated Security=True;TrustServerCertificate=True;Encrypt=True;");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
