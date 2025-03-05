namespace AbcloudzWebAPI.Modules.Users.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    public IMediator Mediator { get; }
    private readonly ILogger<UserController> _logger;

    public UserController(IMediator mediator)
    {
        Mediator = mediator;
    }

    public UserController(ILogger<UserController> logger)
    {
        _logger = logger;
    }

    [HttpPost("create")]
    public async Task<Guid> Create([FromBody] string email)
    {
        return await Mediator.Send(new CreateUserCommand(new UserCreateDto{ Email = email }));
    }

    [HttpGet("list")]
    public async Task<UserInListDto> GetAll()
    {
        return await Mediator.Send(new GetUserListQuery());
    }

    [HttpGet(":id")]
    public async Task<UserDto> Get([FromRoute] Guid id)
    {
        return await Mediator.Send(new GetUserQuery(id));
    }
}
