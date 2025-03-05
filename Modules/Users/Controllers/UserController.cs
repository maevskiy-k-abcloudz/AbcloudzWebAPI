namespace AbcloudzWebAPI.Modules.Users.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<UserController> _logger;

    public UserController(ILogger<UserController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpPost("create")]
    public async Task<Guid> Create([FromBody] string email)
    {
        return await _mediator.Send(new CreateUserCommand(new UserCreateDto{ Email = email }));
    }

    [HttpGet("list")]
    public async Task<UserInListDto> GetAll()
    {
        return await _mediator.Send(new GetUserListQuery());
    }

    [HttpGet(":id")]
    public async Task<UserDto> Get([FromRoute] Guid id)
    {
        return await _mediator.Send(new GetUserQuery(id));
    }
}
