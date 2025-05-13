using AbcloudzWebAPI.Contracts.Dto;
using AbcloudzWebAPI.Contracts.Interfaces;
using AbcloudzWebAPI.Contracts.Models;
using AbcloudzWebAPI.Contracts.Models.User.Add;
using AbcloudzWebAPI.Contracts.Models.User.Get;
using AbcloudzWebAPI.Contracts.Models.User.GetAll;
using AbcloudzWebAPI.Contracts.Models.User.Remove;
using AbcloudzWebAPI.Contracts.Models.User.Update;
using Microsoft.AspNetCore.Mvc;

namespace AbcloudzWebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController(IUserService userService) : ControllerBase
{
    [HttpPost("Get")]
    public ActionResult<GetUserResponse> Get([FromBody] GetUserRequest request)
    {
        var response = userService.User(request);
        return Ok(response);
    }
    
    [HttpPost("GetAll")]
    public ActionResult<GetAllUserResponse> GetAll([FromBody] GetAllUserRequest request)
    {
        var response = userService.Users(request);
        return Ok(response);
    }
    
    [HttpPost("Add")]
    public ActionResult<AddUserResponse> Add([FromBody] AddUserRequest request)
    {
        var response = userService.AddUser(request);
        return StatusCode(StatusCodes.Status201Created, response);
    }
    
    [HttpPost("Remove")]
    public ActionResult<RemoveUserResponse> Remove([FromBody] RemoveUserRequest request)
    {
        var response = userService.RemoveUser(request);
        return Ok(response);
    }
    
    [HttpPost("Update")]
    public ActionResult<UpdateUserResponse> Update([FromBody] UpdateUserRequest request)
    {
        var response = userService.UpdateUser(request);
        return Ok(response);
    }
}