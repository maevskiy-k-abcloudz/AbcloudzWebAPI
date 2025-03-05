namespace AbcloudzWebAPI.Modules.Users.Handlers.Update;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(x => x.Dto.Id).NotEmpty();
        RuleFor(x => x.Dto.Email).NotEmpty();
    }
}
