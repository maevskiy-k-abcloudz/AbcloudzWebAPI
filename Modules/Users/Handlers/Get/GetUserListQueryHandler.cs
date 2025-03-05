namespace AbcloudzWebAPI.Modules.Users.Handlers.Get;

public class GetUserListQueryHandler : QueryHandler<GetUserListQuery, UserInListDto>
{
    private readonly MainContext _context;

    public GetUserListQueryHandler(MainContext context)
    {
        _context = context;
    }

    protected override async Task<UserInListDto> HandleAsync(GetUserListQuery query, CancellationToken cancellationToken)
    {
        var result = await _context.Users
            .Select(x => new UserDto()
            {
                Id = x.Id,
                Email = x.Email,
            })
            .ToListAsync(cancellationToken);

        return new UserInListDto { Users = result };
    }
}
