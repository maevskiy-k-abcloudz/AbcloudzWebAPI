using AbcloudzWebAPI.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace AbcloudzWebAPI.DataAccess
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
           : base(options) { }

        public DbSet<UserEntity> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<UserEntity>()
                .HasKey(x => x.UserId);
        }
    }
}
