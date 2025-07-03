using AbcloudzWebAPI.EntityModels;
using Microsoft.EntityFrameworkCore;

namespace AbcloudzWebAPI.DB
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(x =>
            {
                x.HasKey(x => x.Id);
                x.HasIndex(x => x.PhoneNumber)
                    .IsUnique();
                x.HasIndex(x => x.Email)
                    .IsUnique();
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
