using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerApi
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
        var result = await _userService.UpdateUserAsync(GetUserId(), user);
        return Ok(result);
    }
    
    [Authorize]
    [HttpDelete]
    [Route("delete")]
    public async Task<IActionResult> DeleteAsync()
    {
        var result = await _userService.DelteUserAsync(GetUserId());
        return Ok(result);
    }

    [Authorize]
    [HttpGet]
    [Route("get")]
    public async Task<IActionResult> GetUserById()
    {
        var result = await _userService.GetUserAsync(GetUserId());
        return Ok(result);
    }

}