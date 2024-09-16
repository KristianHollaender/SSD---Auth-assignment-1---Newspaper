using BasicAuthAPI.Core.Service.Interfaces;
using BasicAuthAPI.DTOs;
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
    [Route("/login/")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserDTO createUserDto)
    {
        try
        {
            return Ok(await _userService.CreateUser(createUserDto));
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
            return Ok(await _userService.GetAllUsers());
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
            return Ok(await _userService.GetUserById(userId));
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
            return Ok(await _userService.DeleteUser());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
}