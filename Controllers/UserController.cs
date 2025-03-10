using AbcloudzWebAPI.Infrastructure.Services;
using AbcloudzWebAPI.Models.DataTransfer.Request;
using AbcloudzWebAPI.Models.DataTransfer.Response;
using AbcloudzWebAPI.Models.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AbcloudzWebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{

    private readonly ILogger<UserController> _logger;
    private readonly IUserService _userService;

    public UserController(
        ILogger<UserController> logger,
        IUserService userService)
    {
        _logger = logger;

        _userService = userService;
    }

    [AllowAnonymous]
    [HttpGet("user/{id}")]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        var user = await _userService.GetUserById(id);

        // Automapper map

        var response = new UserResponse
        {
            PhoneNumber = user.PhoneNumber,
            UserName = user.UserName,
        };

        return Ok(response);
    }

    [AllowAnonymous]
    [HttpPost("user")]
    public async Task<IActionResult> Create([FromBody] CreateUserRequest request)
    {
        var user = new User
        {
            UserName = request.UserName,
            PhoneNumber = request.PhoneNumber,
            UserEmail = request.UserEmail
        };

        var createdUser = await _userService.CreateUser(user, request.Password);

        var response = new UserResponse
        {
            PhoneNumber = createdUser.PhoneNumber,
            UserName = createdUser.UserName,
        };

        return Ok(response);
    }

    [HttpPut("user")]
    public async Task<IActionResult> Update([FromBody] UpdateUserRequest request)
    {
        var user = new User
        {
            UserId = request.UserId,
            PhoneNumber = request.PhoneNumber
        };

        await _userService.UpdateUser(user);

        return Ok();
    }

    [HttpDelete("user/{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        await _userService.DeleteUser(id);

        return Ok();
    }

    [HttpGet("users/{skip}/{take}")]
    public async Task<IActionResult> GetAllUsers([FromRoute] int skip, int take)
    {
        var result = await _userService.GetAllUsersPaged(skip, take);

        var response = new UserCollectionResponse
        {
            Users = new List<UserResponse>(result.Count),
        };

        foreach(var user in result)
        {
            response.Users.Add(new UserResponse
            {
                UserName = user.UserName,
                PhoneNumber = user.PhoneNumber
            });
        }

        return Ok(response);
    }
}
