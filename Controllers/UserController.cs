using AbcloudzWebAPI.DTO;
using AbcloudzWebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace AbcloudzWebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{

    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<PagedResponse<UserDTO>> Get([FromQuery] SearchUsersDTO searchModel)
    {
        return await _userService.GetAsync(searchModel);
    }

    [HttpGet("{id}")]
    public async Task<UserDTO> Get(Guid id)
    {
        return await _userService.GetAsync(id);
    }

    [HttpPost]
    public async Task<Guid> Create(CreateUserDTO model)
    {
        return await _userService.CreateAsync(model);
    }

    [HttpPatch]
    public async Task Update(UpdateUserDTO model)
    {
        await _userService.UpdateAsync(model);
    }

    [HttpDelete("{id}")]
    public async Task Delete(Guid id)
    {
        await _userService.DeleteAsync(id);
    }
}
