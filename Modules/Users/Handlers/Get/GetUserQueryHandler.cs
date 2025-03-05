namespace AbcloudzWebAPI.Modules.Users.Handlers.Get;

public class GetUserQueryHandler : QueryHandler<GetUserQuery, UserDto?>
{
    private readonly MainContext _context;

    public GetUserQueryHandler(MainContext context)
    {
        _context = context;
    }

    protected override async Task<UserDto?> HandleAsync(GetUserQuery query, CancellationToken cancellationToken)
    {
        var result = await _context.Users
            .Where(x => x.Id == query.UserId)
            .Select(x => new UserDto()
            {
                Id = x.Id,
                Email = x.Email,
            })
            .FirstOrDefaultAsync(cancellationToken);

        return result;
    }
}
