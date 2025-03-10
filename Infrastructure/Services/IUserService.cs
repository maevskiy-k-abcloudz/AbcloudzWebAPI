using AbcloudzWebAPI.Models.Domain;

namespace AbcloudzWebAPI.Infrastructure.Services
{
    public interface IUserService
    {
        Task<User> CreateUser(User newUser, string password);

        Task DeleteUser(int id);

        Task<User> GetUserById(int id);

        Task<List<User>> GetAllUsersPaged(int skip = 0, int take = 10);

        Task UpdateUser(User updatedUser);
    }
}
