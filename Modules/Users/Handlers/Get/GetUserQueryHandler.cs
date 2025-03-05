namespace AbcloudzWebAPI.Modules.Users.Handlers.Get;

public class GetUserQueryHandler : QueryHandler<GetUserQuery, UserDto>
{
    protected override Task<UserDto> HandleAsync(GetUserQuery query, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}