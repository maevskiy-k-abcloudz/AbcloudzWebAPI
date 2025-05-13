using AbcloudzWebAPI.Contracts.Dto;
using AbcloudzWebAPI.Contracts.Interfaces;
using AbcloudzWebAPI.Contracts.Models.User.Add;
using AbcloudzWebAPI.Contracts.Models.User.Get;
using AbcloudzWebAPI.Contracts.Models.User.GetAll;
using AbcloudzWebAPI.Contracts.Models.User.Remove;
using AbcloudzWebAPI.Contracts.Models.User.Update;
using AbcloudzWebAPI.Domain.Entities;
using AbcloudzWebAPI.Domain.Interfaces;
using AbcloudzWebAPI.Contracts.Enums;
using AbcloudzWebAPI.Contracts.Exceptions;

namespace AbcloudzWebAPI.Application.Services;

public class UserService : BaseService, IUserService
{
    private readonly IApplicationDbContext _applicationDbContext;

    public UserService(IApplicationDbContext applicationDbContext, ILogger<UserService> logger)
        : base(logger)
    {
        _applicationDbContext = applicationDbContext;
    }

    public GetAllUserResponse Users(GetAllUserRequest request)
    {
        return ExecuteSave(() =>
        {
            var query = _applicationDbContext.Users
                .Where(u => !u.IsDeleted);

            if (!string.IsNullOrEmpty(request.Identifier))
            {
                var identifierLower = request.Identifier.ToLower();
                query = query.Where(u =>
                    u.FirstName.ToLower().Contains(identifierLower) ||
                    u.LastName.ToLower().Contains(identifierLower) ||
                    u.Email.ToLower().Contains(identifierLower));
            }

            if (request.IsActive.HasValue)
            {
                query = query.Where(u => u.IsActive == request.IsActive.Value);
            }

            query = request.SortingType switch
            {
                SortingType.Id => request.Sorting == Sorting.Asc
                    ? query.OrderBy(u => u.Id)
                    : query.OrderByDescending(u => u.Id),
                SortingType.FirstName => request.Sorting == Sorting.Asc
                    ? query.OrderBy(u => u.FirstName)
                    : query.OrderByDescending(u => u.FirstName),
                SortingType.LastName => request.Sorting == Sorting.Asc
                    ? query.OrderBy(u => u.LastName)
                    : query.OrderByDescending(u => u.LastName),
                SortingType.Email => request.Sorting == Sorting.Asc
                    ? query.OrderBy(u => u.Email)
                    : query.OrderByDescending(u => u.Email),
                _ => query.OrderBy(u => u.Id)
            };

            var totalCount = query.Count();

            var users = query
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    IsActive = u.IsActive,
                    CreatedAt = u.CreatedAt,
                    UpdatedAt = u.UpdatedAt
                })
                .ToList();

            return new GetAllUserResponse(users, totalCount);
        });
    }

    public AddUserResponse AddUser(AddUserRequest request)
    {
        return ExecuteSave(() =>
        {
            if (string.IsNullOrEmpty(request.Email))
            {
                throw new BusinessException("Email is required.");
            }

            if (_applicationDbContext.Users.Any(u => u.Email.ToLower() == request.Email.ToLower() && !u.IsDeleted))
            {
                throw new BusinessException("Email must be unique.");
            }

            var userEntity = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Password = request.Password,
                IsActive = true
            };

            var addedUser = _applicationDbContext.AddUser(userEntity);
            return new AddUserResponse(addedUser.Id);
        });
    }

    public RemoveUserResponse RemoveUser(RemoveUserRequest request)
    {
        return ExecuteSave(() =>
        {
            if (!_applicationDbContext.Users.Any(u => u.Id == request.Id && !u.IsDeleted))
            {
                throw new BusinessException($"User with ID {request.Id} does not exist.");
            }

            var deletedId = _applicationDbContext.RemoveUser(request.Id);
            return new RemoveUserResponse(deletedId);
        });
    }

    public UpdateUserResponse UpdateUser(UpdateUserRequest request)
    {
        return ExecuteSave(() =>
        {
            if (!_applicationDbContext.Users.Any(u => u.Id == request.Id && !u.IsDeleted))
            {
                throw new BusinessException($"User with ID {request.Id} does not exist.");
            }

            if (request.Email != null && _applicationDbContext.Users.Any(u => u.Email.ToLower() == request.Email.ToLower() && u.Id != request.Id && !u.IsDeleted))
            {
                throw new BusinessException("Email must be unique.");
            }

            var userEntity = _applicationDbContext.User(request.Id);
            
            userEntity.FirstName = request.FirstName ?? userEntity.FirstName;
            userEntity.LastName = request.LastName ?? userEntity.LastName;
            userEntity.Email = request.Email ?? userEntity.Email;
            userEntity.Password = request.Password ?? userEntity.Password;
            userEntity.IsActive = request.IsActive ?? userEntity.IsActive;
            userEntity.IsDeleted = request.IsDeleted ?? userEntity.IsDeleted;

            var updatedUser = _applicationDbContext.UpdateUser(userEntity);
            return new UpdateUserResponse(updatedUser.Id);
        });
    }

    public GetUserResponse User(GetUserRequest request)
    {
        return ExecuteSave(() =>
        {
            if (!_applicationDbContext.Users.Any(u => u.Id == request.Id && !u.IsDeleted))
            {
                throw new BusinessException($"User with ID {request.Id} does not exist.");
            }

            var userEntity = _applicationDbContext.User(request.Id);
            
            var userDto = new UserDto
            {
                Id = userEntity.Id,
                FirstName = userEntity.FirstName,
                LastName = userEntity.LastName,
                Email = userEntity.Email,
                IsActive = userEntity.IsActive,
                CreatedAt = userEntity.CreatedAt,
                UpdatedAt = userEntity.UpdatedAt
            };

            return new GetUserResponse(userDto);
        });
    }
}