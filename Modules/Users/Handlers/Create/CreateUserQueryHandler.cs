namespace AbcloudzWebAPI.Modules.Users.Handlers.Create;

public class CreateUserQueryHandler : CommandHandler<CreateUserCommand, Guid>
{
    private readonly MainContext _context;

    public CreateUserQueryHandler(ICommandEventQueueWriter commandEvents, MainContext context)
        : base(commandEvents)
    {
        _context = context;
    }

    protected override async Task<Guid> HandleAsync(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var newUser = new UserEntity()
        {
            Id = Guid.NewGuid(),
            Email = command.Dto.Email,
        };
        _context.Users.Add(newUser);

        await _context.SaveChangesAsync(cancellationToken);

        return newUser.Id;
    }
}
