using AbcloudzWebAPI.Infrastructure.Attributes;

namespace AbcloudzWebAPI.Contracts.Models.User.Update;

public class UpdateUserRequest
{
    [RequiredNonEmpty]
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public bool? IsActive { get; set; }
    public bool? IsDeleted { get; set; }
}