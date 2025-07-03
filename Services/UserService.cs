using AbcloudzWebAPI.DB;
using AbcloudzWebAPI.DTO;
using AbcloudzWebAPI.EntityModels;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Text;

namespace AbcloudzWebAPI.Services
{

    public interface IUserService
    {
        Task<UserDTO> GetAsync(Guid id);
        Task<PagedResponse<UserDTO>> GetAsync(SearchUsersDTO model);
        Task<Guid> CreateAsync(CreateUserDTO model);
        Task UpdateAsync(UpdateUserDTO model);
        Task DeleteAsync(Guid id);
    }

    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> CreateAsync(CreateUserDTO model)
        {
            if (await _context.Users.AnyAsync(x => x.Email == model.Email || x.PhoneNumber == model.PhoneNumber))
            {
                throw new ArgumentException("Email and phone numbers should be unique");
            }

            var user = new User
            {
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
            };

            user.PasswordHash = GetHash(model.Password);

            await _context.AddAsync(user);
            await _context.SaveChangesAsync();

            return user.Id;
        }

        public async Task DeleteAsync(Guid id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
            {
                throw new KeyNotFoundException();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<UserDTO> GetAsync(Guid id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
            {
                throw new KeyNotFoundException();
            }

            return ToDTO(user);
        }

        public async Task<PagedResponse<UserDTO>> GetAsync(SearchUsersDTO model)
        {
            var query = _context.Users.AsQueryable();

            model.Term = model.Term?.Trim();

            if (!string.IsNullOrWhiteSpace(model.Term))
            {
                query = query.Where(x => x.LastName.Contains(model.Term) || x.FirstName.Contains(model.Term) || x.Email.Contains(model.Term) || x.PhoneNumber.Contains(model.Term));
            }

            Expression<Func<User, object>> orderExpression = model.Field switch
            {
                UserSortFields.PhoneNumber => (user) => user.PhoneNumber,
                UserSortFields.Email => (user) => user.Email,
                UserSortFields.FirstName => (user) => user.FirstName,
                UserSortFields.LastName => (user) => user.LastName,

            };

            query = model.IsAscending ? query.OrderBy(orderExpression) : query.OrderByDescending(orderExpression);

            var entities = await query
                .AsNoTracking()
                .Skip((model.Page - 1) * model.PageSize)
                .Take(model.PageSize)
                .ToListAsync();

            var count = await query.CountAsync();

            var result = new PagedResponse<UserDTO>
            {
                Items = entities.Select(ToDTO).ToList(),
                TotalCount = count,
            };

            return result;
        }

        public async Task UpdateAsync(UpdateUserDTO model)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == model.Id);

            if (user == null)
            {
                throw new KeyNotFoundException();
            }

            user.FirstName = model.FirstName ?? user.FirstName;
            user.LastName = model.FirstName ?? user.LastName;
            user.Email = model.FirstName ?? user.Email;
            user.PhoneNumber = model.FirstName ?? user.PhoneNumber;
            user.PasswordHash = model.Password != null ? GetHash(model.Password) : user.PasswordHash;

            if (await _context.Users.AnyAsync(x => (x.Email == user.Email || x.PhoneNumber == user.PhoneNumber) && x.Id != user.Id))
            {
                throw new ArgumentException("Email and phone numbers should be unique");
            }

            await _context.SaveChangesAsync();
        }

        private static UserDTO ToDTO(User user)
        => new UserDTO
        {
            Id = user.Id,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            PhoneNumber = user.PhoneNumber,
        };

        private static byte[] GetHash(string value)
        {
            var tmpSource = Encoding.ASCII.GetBytes(value);
            var hash = System.Security.Cryptography.MD5.HashData(tmpSource);
            return hash;
        }
    }
}
