using AbcloudzWebAPI.AbcloudsWebAPI.DataLayer.Entities;
using AbcloudzWebAPI.AbcloudsWebAPI.DataLayer.Models;

namespace AbcloudzWebAPI.AbcloudsWebAPI.DataLayer.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetUsersAsync(UserSearchModel userSearchModel);

    Task<User?> GetUserByEmail(string email);

    Task<bool> AddUserAsync(User user);
}
