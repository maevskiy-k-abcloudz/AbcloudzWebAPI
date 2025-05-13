using AbcloudzWebAPI.Domain.Entities;

namespace AbcloudzWebAPI.Domain.Interfaces;

public interface IApplicationDbContext
{
    List<User> Users { get; }

    User AddUser(User user);

    int RemoveUser(int id);

    User UpdateUser(User user);

    User User(int id);
}