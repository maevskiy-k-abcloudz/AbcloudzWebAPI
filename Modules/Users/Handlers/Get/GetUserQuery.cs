namespace AbcloudzWebAPI.Modules.Users.Handlers.Get;

public class GetUserQuery : Query<UserDto>
{
    public Guid UserId { get; }

    public GetUserQuery(Guid userId)
    {
        UserId = userId;
    }
}
