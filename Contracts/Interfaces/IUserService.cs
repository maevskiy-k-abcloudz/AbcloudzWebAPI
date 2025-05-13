using AbcloudzWebAPI.Contracts.Models.User.Add;
using AbcloudzWebAPI.Contracts.Models.User.Get;
using AbcloudzWebAPI.Contracts.Models.User.GetAll;
using AbcloudzWebAPI.Contracts.Models.User.Remove;
using AbcloudzWebAPI.Contracts.Models.User.Update;

namespace AbcloudzWebAPI.Contracts.Interfaces;

public interface IUserService
{
    GetAllUserResponse Users(GetAllUserRequest request);
    GetUserResponse User(GetUserRequest request);
    AddUserResponse AddUser(AddUserRequest request);
    RemoveUserResponse RemoveUser(RemoveUserRequest request);
    UpdateUserResponse UpdateUser(UpdateUserRequest request);
}