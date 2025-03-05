namespace AbcloudzWebAPI.Modules.Users.Handlers.Get;

public class GetUserQueryValidator : AbstractValidator<GetUserQuery>
{
    public GetUserQueryValidator()
    {
        // RuleSet("Names", () =>
        // {
        //     RuleFor(x => x.Surname).NotNull();
        //     RuleFor(x => x.Forename).NotNull();
        // });

        RuleFor(x => x.UserId).Empty();
    }
}
