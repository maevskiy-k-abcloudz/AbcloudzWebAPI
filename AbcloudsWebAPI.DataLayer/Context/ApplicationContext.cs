using AbcloudzWebAPI.AbcloudsWebAPI.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace AbcloudzWebAPI.AbcloudsWebAPI.DataLayer.Context;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
}
