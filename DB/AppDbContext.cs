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
    }
}
