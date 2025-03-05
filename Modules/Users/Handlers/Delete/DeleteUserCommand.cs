namespace AbcloudzWebAPI.Modules.Users.Handlers.Delete;

public class DeleteUserCommand : Command
{
    public Guid UserId { get; }

    public DeleteUserCommand(Guid userId)
    {
        UserId = userId;
    }
}
