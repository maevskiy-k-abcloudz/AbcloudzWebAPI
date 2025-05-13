using AbcloudzWebAPI.Contracts.Dto;

namespace AbcloudzWebAPI.Contracts.Models.User.GetAll;

public record GetAllUserResponse(List<UserDto> Users, int TotalCount);