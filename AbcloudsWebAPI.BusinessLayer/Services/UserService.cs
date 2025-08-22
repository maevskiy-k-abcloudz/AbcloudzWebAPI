using AbcloudzWebAPI.AbcloudsWebAPI.BusinessLayer.DTOs;
using AbcloudzWebAPI.AbcloudsWebAPI.DataLayer.Entities;
using AbcloudzWebAPI.AbcloudsWebAPI.DataLayer.Repositories;

namespace AbcloudzWebAPI.AbcloudsWebAPI.BusinessLayer.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<bool> AddUserAsync(AddUserDto addUserDto)
    {
        try
        {
            if (string.IsNullOrEmpty(addUserDto.Firstname))
            {
                throw new Exception("Firstname is required");
            }

            var existingUser = await _userRepository
                .GetUserByEmail(addUserDto.Email)
                .ConfigureAwait(false);

            if (existingUser != null)
            {
                throw new Exception("User with given email already exists");
            }

            // Map
            User userToAdd = new()
            {
                Id = Guid.NewGuid(),
                Firstname = addUserDto.Firstname,
                Lastname = addUserDto.Lastname,
                Email = addUserDto.Email,
                DOB = addUserDto.DOB
            };

            await _userRepository.AddUserAsync(userToAdd).ConfigureAwait(false);

            return true;
        }
        catch (Exception ex)
        {
            // handler error
            return false;
        }
    }
}
