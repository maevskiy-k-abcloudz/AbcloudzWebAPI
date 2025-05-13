using AbcloudzWebAPI.Infrastructure.Attributes;

namespace AbcloudzWebAPI.Contracts.Models.User.Remove;

public class RemoveUserRequest
{
    [RequiredNonEmpty]
    public int Id { get; set; }
}