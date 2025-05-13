using AbcloudzWebAPI.Domain.Entities;
using AbcloudzWebAPI.Domain.Interfaces;

namespace AbcloudzWebAPI.DB;

public class ApplicationDbContext : IApplicationDbContext
{
    private Dictionary<int, User> _users = new Dictionary<int, User>();
    
    public ApplicationDbContext()
    {
        _users.Add(1, new User 
        { 
            Id = 1, 
            FirstName = "John", 
            LastName = "Doe", 
            Email = "john.doe@example.com",
            Password = "Password123!",
            IsActive = true,
            IsDeleted = false,
            CreatedAt = DateTimeOffset.UtcNow.AddDays(-30),
            UpdatedAt = DateTimeOffset.UtcNow.AddDays(-30)
        });
        
        _users.Add(2, new User 
        { 
            Id = 2, 
            FirstName = "Jane", 
            LastName = "Smith", 
            Email = "jane.smith@example.com",
            Password = "SecurePass456@",
            IsActive = true,
            IsDeleted = false,
            CreatedAt = DateTimeOffset.UtcNow.AddDays(-15),
            UpdatedAt = DateTimeOffset.UtcNow.AddDays(-15)
        });
        
        _users.Add(3, new User 
        { 
            Id = 3, 
            FirstName = "Bob", 
            LastName = "Johnson", 
            Email = "bob.johnson@example.com",
            Password = "MyPass789#",
            IsActive = false,
            IsDeleted = false,
            CreatedAt = DateTimeOffset.UtcNow.AddDays(-5),
            UpdatedAt = DateTimeOffset.UtcNow.AddDays(-5)
        });
    }
    
    public List<User> Users { get => _users.Values.ToList(); }

    public User AddUser(User user)
    {
        user.Id = _users.Count + 1;
        user.IsActive = user.IsActive;
        user.IsDeleted = false;
        user.CreatedAt = DateTimeOffset.UtcNow;
        user.UpdatedAt = DateTimeOffset.UtcNow;
        _users.Add(user.Id, user);
        return user;
    }

    public int RemoveUser(int id)
    {
        _users.Remove(id);
        return id;
    }

    public User UpdateUser(User user)
    {
        _users[user.Id].FirstName = user.FirstName;
        _users[user.Id].LastName = user.LastName;
        _users[user.Id].Email = user.Email;
        _users[user.Id].Password = user.Password;
        _users[user.Id].IsActive = user.IsActive;
        _users[user.Id].IsDeleted = user.IsDeleted;
        _users[user.Id].UpdatedAt = DateTimeOffset.UtcNow;
        return _users[user.Id];
    }
    
    public User User(int id)
    {
        return _users[id];
    }
}