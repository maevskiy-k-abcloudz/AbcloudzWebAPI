using AbcloudzWebAPI.Infrastructure.Attributes;

namespace AbcloudzWebAPI.Contracts.Models.User.Get;

public class GetUserRequest
{
    [RequiredNonEmpty]
    public int Id { get; set; }
}