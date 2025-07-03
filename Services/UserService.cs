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
                // TODO return
            }

            var user = new User
            {
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
            };

            var tmpSource = ASCIIEncoding.ASCII.GetBytes(model.Password);
            var hash = System.Security.Cryptography.MD5.HashData(tmpSource);

            user.PasswordHash = hash;

            await _context.AddAsync(user);
            await _context.SaveChangesAsync();

            return user.Id;
        }

        public async Task DeleteAsync(Guid id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
            {
                // TODO return not found
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<UserDTO> GetAsync(Guid id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
            {
                // TODO
            }

            return ToDTO(user);
        }

        public async Task<PagedResponse<UserDTO>> GetAsync(SearchUsersDTO model)
        {
            var query = _context.Users.AsQueryable();

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

        public Task UpdateAsync(UpdateUserDTO model)
        {
            throw new NotImplementedException();
        }

        private UserDTO ToDTO(User user)
        => new UserDTO
        {
            Id = user.Id,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            PhoneNumber = user.PhoneNumber,
        };
    }
}
