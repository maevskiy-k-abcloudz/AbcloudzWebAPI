using AbcloudzWebAPI.DataAccess.Models;
using AbcloudzWebAPI.Infrastructure.Repositories;
using AbcloudzWebAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace AbcloudzWebAPI.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationContext _context;

        public UserRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<User> CreateUser(User newUser, string pwdHash)
        {
            var dbUser = new UserEntity
            {
                UserId = newUser.UserId,
                UserEmail = newUser.UserEmail,
                PhoneNumber = newUser.PhoneNumber,
                UserName = newUser.UserName,
                PasswordHash = pwdHash,
            };

            var entity = await _context.Users.AddAsync(dbUser);

            await _context.SaveChangesAsync();

            return MapFromEntity(entity.Entity);
        }

        public async Task DeleteUser(int id)
        {
            var user = await FindUserById(id);

            _context.Users.Remove(user);

            await _context.SaveChangesAsync();
        }

        public async Task<List<User>> GetAllUsersPaged(int skip = 0, int take = 10)
        {
            var users = await _context.Users.Skip(skip).Take(take).ToListAsync();

            var result = users.Select(MapFromEntity).ToList();

            return result;
        }

        public async Task<User> GetUserById(int id)
        {
            var user = await FindUserById(id);

            return MapFromEntity(user);
        }

        public async Task UpdateUser(User updatedUser)
        {
            var dbUser = await FindUserById(updatedUser.UserId);

            dbUser.PhoneNumber = updatedUser.PhoneNumber;

            await _context.SaveChangesAsync();
        }

        private User MapFromEntity(UserEntity entity)
        {
            return new User
            {
                PhoneNumber = entity.PhoneNumber,
                UserEmail = entity.UserEmail,
                UserId = entity.UserId,
                UserName = entity.UserName
            };
        }

        private async Task<UserEntity> FindUserById(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == id);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            return user;
        }
    }
}
