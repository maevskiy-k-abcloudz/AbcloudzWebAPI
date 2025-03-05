namespace AbcloudzWebAPI.Infrastructure;

public class MainContext : DbContext
{
    public MainContext(DbContextOptions<MainContext> options)
        : base(options)
    {

    }
    public DbSet<UserEntity> Users;
}
