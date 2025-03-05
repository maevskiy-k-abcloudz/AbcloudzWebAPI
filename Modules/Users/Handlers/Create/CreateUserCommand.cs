namespace AbcloudzWebAPI.Modules.Users.Handlers.Create;

public class CreateUserCommand : Command<Guid>
{
    public UserCreateDto Dto { get; }

    public CreateUserCommand(UserCreateDto dto)
    {
        Dto = dto;
    }
}
