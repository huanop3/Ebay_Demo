//ef-context-ip
using Microsoft.EntityFrameworkCore;

namespace webapi_blazor.Models {
    public class StoreCybersoftContext : DbContext {
        public StoreCybersoftContext() { }
        public StoreCybersoftContext(DbContextOptions<StoreCybersoftContext> options) : base (options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=127.0.0.1,1434;Database=StoreCybersoft;User Id=sa;Password=HuanNC231@;TrustServerCertificate=Yes;");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Products> Products { get; set; }
    }
}