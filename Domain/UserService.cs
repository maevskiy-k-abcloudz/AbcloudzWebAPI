using AbcloudzWebAPI.Infrastructure.Repositories;
using AbcloudzWebAPI.Infrastructure.Services;
using AbcloudzWebAPI.Models.Domain;

namespace AbcloudzWebAPI.Domain
{
    public class UserService : IUserService
    {
        private readonly ISecurityService _securityService;
        private readonly IUserRepository _userRepository;

        public UserService(
            ISecurityService securityService,
            IUserRepository userRepository)
        {
            _securityService = securityService;

            _userRepository = userRepository;
        }

        public async Task<User> CreateUser(User newUser, string password)
        {
            var pwdHash = _securityService.HashPwd(password);

            var createdUser = await _userRepository.CreateUser(newUser, pwdHash);

            return createdUser;
        }

        public async Task DeleteUser(int id)
        {
            await _userRepository.DeleteUser(id);
        }

        public async Task<List<User>> GetAllUsersPaged(int skip = 0, int take = 10)
        {
            return await _userRepository.GetAllUsersPaged(skip, take);
        }

        public async Task<User> GetUserById(int id)
        {
            return await _userRepository.GetUserById(id);
        }

        public async Task UpdateUser(User updatedUser)
        {
            await _userRepository.UpdateUser(updatedUser);
        }
    }
}
