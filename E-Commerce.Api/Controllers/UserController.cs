using System.Security.Claims;
using System.Threading.Tasks;
using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    [Route("create")]
    public async Task<IActionResult> CreateAsync([FromBody] RegisterUserDto user)
    {
        var result = await _userService.InsertUserAsync(user);
        return Ok(result);
    }
    
    [Authorize]
    [HttpPut]
    [Route("update")]
    public async Task<IActionResult> UpdateAsync([FromBody] RegisterUserDto user)
    {
        int userId = Convert.ToInt16(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        
        var result = await _userService.UpdateUserAsync(userId, user);
        return Ok(result);
    }
    
    [Authorize]
    [HttpDelete]
    [Route("delete")]
    public async Task<IActionResult> DeleteAsync()
    {
        int userId = Convert.ToInt16(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

        var result = await _userService.DelteUserAsync(userId);
        return Ok(result);
    }

    [Authorize]
    [HttpGet]
    [Route("get")]
    public async Task<IActionResult> GetUserById()
    {
        int userId = Convert.ToInt16(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        
        var result = await _userService.GetUserAsync(userId);
        return Ok(result);
    }

}