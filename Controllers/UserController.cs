using AbcloudzWebAPI.AbcloudsWebAPI.BusinessLayer.DTOs;
using AbcloudzWebAPI.AbcloudsWebAPI.BusinessLayer.Services;
using AbcloudzWebAPI.AbcloudsWebAPI.DataLayer.Models;
using AbcloudzWebAPI.AbcloudsWebAPI.DataLayer.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AbcloudzWebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{

    private readonly ILogger<UserController> _logger;
    private readonly IUserRepository _userRepository;
    private readonly IUserService _userService;

    public UserController(
        IUserRepository userRepository,
        IUserService userService,
        ILogger<UserController> logger)
    {
        _logger = logger;
        _userRepository = userRepository;
        _userService = userService;
    }

    [HttpGet("list")]
    public async Task<IActionResult> Get([FromQuery] UserSearchModel userSearchModel)
    {
        var users = await _userRepository.GetUsersAsync(userSearchModel).ConfigureAwait(false);
        return Ok(users);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] AddUserDto addUserDto)
    {
        var result = await _userService
            .AddUserAsync(addUserDto)
            .ConfigureAwait(false);

        return result ? Ok(result) : BadRequest("Error while adding user");
    }
}
