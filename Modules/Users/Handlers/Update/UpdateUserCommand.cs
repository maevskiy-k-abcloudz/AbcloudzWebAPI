namespace AbcloudzWebAPI.Modules.Users.Handlers.Update;

public class UpdateUserCommand : Command
{
    public UserDto Dto { get; }

    public UpdateUserCommand(UserDto dto)
    {
        Dto = dto;
    }
}
