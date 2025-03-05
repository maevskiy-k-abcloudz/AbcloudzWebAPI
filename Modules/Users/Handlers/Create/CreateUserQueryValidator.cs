namespace AbcloudzWebAPI.Modules.Users.Handlers.Create;

public class GetUserQueryValidator : AbstractValidator<CreateUserCommand>
{
    public GetUserQueryValidator()
    {
        RuleFor(x => x.Dto.Email).NotEmpty();
    }
}
