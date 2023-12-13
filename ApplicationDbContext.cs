using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;

namespace WEB
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<AppS> Apps { get; set; }

        public ApplicationDbContext()
        {
            Database.EnsureCreated();
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // !!!
            optionsBuilder.UseSqlite("Data Source=Database//Website.db");
        }
    }
}
