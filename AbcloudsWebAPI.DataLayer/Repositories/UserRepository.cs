using AbcloudzWebAPI.AbcloudsWebAPI.DataLayer.Context;
using AbcloudzWebAPI.AbcloudsWebAPI.DataLayer.Entities;
using AbcloudzWebAPI.AbcloudsWebAPI.DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace AbcloudzWebAPI.AbcloudsWebAPI.DataLayer.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationContext _appContext;
    private const int DefaultTake = 100;

    public UserRepository(ApplicationContext context)
    {
        _appContext = context;
    }

    // pagination, sorting
    public async Task<IEnumerable<User>> GetUsersAsync(UserSearchModel userSearchModel)
    {
        // Searching

        IQueryable<User> resultUserSet = _appContext.Users;
        if (!string.IsNullOrEmpty(userSearchModel.Firstname))
        {
            resultUserSet = resultUserSet
                .Where(u => u.Firstname.Contains(userSearchModel.Firstname, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrEmpty(userSearchModel.Lastname))
        {
            resultUserSet = resultUserSet
                .Where(u => u.Lastname.Contains(userSearchModel.Lastname, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrEmpty(userSearchModel.Email))
        {
            resultUserSet = resultUserSet
                .Where(u => u.Email.Contains(userSearchModel.Email, StringComparison.OrdinalIgnoreCase));
        }

        if (userSearchModel.DOB != null)
        {
            resultUserSet = resultUserSet.Where(u => u.DOB == userSearchModel.DOB);
        }

        // Sorting
        Sort(userSearchModel.SortBy, ref resultUserSet);

        // Pagination
        return await resultUserSet
            .Skip(userSearchModel.Skip ?? 0)
            .Take(userSearchModel.Take ?? DefaultTake)
            .ToListAsync()
            .ConfigureAwait(false);
    }

    public async Task<bool> AddUserAsync(User user)
    {
        try
        {
            await _appContext.Users.AddAsync(user).ConfigureAwait(false);
            await _appContext.SaveChangesAsync().ConfigureAwait(false);
            return true;
        }
        catch (Exception ex) 
        {
            // log/handle error

            return false;
        }
    }

    public async Task<User?> GetUserByEmail(string email)
    {
        return await _appContext.Users
            .FirstOrDefaultAsync(u => u.Email == email)
            .ConfigureAwait(false);
    }

    private void Sort(string? sortField, ref IQueryable<User> resultUserSet) 
    { 
        var fieldToSort = sortField ?? nameof(User.Firstname);
        switch (fieldToSort)
        {
            case nameof(User.Firstname):
                resultUserSet = resultUserSet.OrderBy(u => u.Firstname);
                break;
            case nameof(User.Lastname):
                resultUserSet = resultUserSet.OrderBy(u => u.Lastname);
                break;
            case nameof(User.DOB):
                resultUserSet = resultUserSet.OrderBy(u => u.DOB);
                break;
        }
    }
}
