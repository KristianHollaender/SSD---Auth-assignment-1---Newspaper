using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BasicAuthAPI.Controllers;

public class UserController : ControllerBase
{

    private readonly IUserService _userService;
    
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser()
    {
        try
        {
            return Ok(await _userService.createUser());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetUsers()
    {
        try
        {
            return Ok(await _userService.getUsers());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpGet]
    [Route("/{userId}")]
    [Authorize]
    public async Task<IActionResult> GetUserById([FromRoute] int userId)
    {
        try
        {
            return Ok(await _userService.getUserById(userId));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    
    [HttpDelete]
    [Authorize]
    public async Task<IActionResult> DeleteUser()
    {
        try
        {
            return Ok(await _userService.deleteUser());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
}