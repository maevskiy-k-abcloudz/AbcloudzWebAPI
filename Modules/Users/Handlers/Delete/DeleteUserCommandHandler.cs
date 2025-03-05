namespace AbcloudzWebAPI.Modules.Users.Handlers.Delete;

public class DeleteUserCommandHandler : CommandHandler<DeleteUserCommand>
{
    private readonly MainContext _context;

    public DeleteUserCommandHandler(ICommandEventQueueWriter commandEvents, MainContext context)
        : base(commandEvents)
    {
        _context = context;
    }

    protected override async Task HandleAsync(DeleteUserCommand command, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .Where(x => x.Id == command.UserId)
            .FirstOrDefaultAsync(cancellationToken);

        if (user != null)
            _context.Users.Remove(user);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
