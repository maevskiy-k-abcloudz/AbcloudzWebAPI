using AbcloudzWebAPI.AbcloudsWebAPI.BusinessLayer.DTOs;

namespace AbcloudzWebAPI.AbcloudsWebAPI.BusinessLayer.Services;

public interface IUserService
{
    Task<bool> AddUserAsync(AddUserDto addUserDto);
}

