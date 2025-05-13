using AbcloudzWebAPI.Contracts.Enums;
using AbcloudzWebAPI.Infrastructure.Attributes;

namespace AbcloudzWebAPI.Contracts.Models.User.Add;

public class AddUserRequest
{
    [RequiredNonEmpty]
    public string FirstName { get; set; }
    [RequiredNonEmpty]
    public string LastName { get; set; }
    [RequiredNonEmpty]
    public string Email { get; set; }
    [RequiredNonEmpty]
    public string Password { get; set; }
}