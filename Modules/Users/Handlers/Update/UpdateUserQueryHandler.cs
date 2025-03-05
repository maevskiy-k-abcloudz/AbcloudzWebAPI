namespace AbcloudzWebAPI.Modules.Users.Handlers.Update;

public class UpdateUserQueryHandler : CommandHandler<UpdateUserCommand>
{
    private readonly MainContext _context;

    public UpdateUserQueryHandler(ICommandEventQueueWriter commandEvents, MainContext context)
        : base(commandEvents)
    {
        _context = context;
    }

    protected override async Task HandleAsync(UpdateUserCommand command, CancellationToken cancellationToken)
    {
        var newUser = await _context.Users
            .Where(x => x.Id == command.Dto.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (newUser == null)
            throw new ArgumentException(nameof(command.Dto.Id));

        newUser.Email = command.Dto.Email;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
