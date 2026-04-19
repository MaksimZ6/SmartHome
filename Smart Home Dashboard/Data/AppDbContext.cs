using Microsoft.EntityFrameworkCore;

namespace Smart_Home_Dashboard.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<SmartDevice> Devices { get; set; }
        public DbSet<SmartLog> Logs { get; set; }
        public DbSet<User> Users { get; set; }
    }
}