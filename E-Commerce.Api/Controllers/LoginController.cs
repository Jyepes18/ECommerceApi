using ECommerce.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class LoginController : ControllerBase
{
    private readonly ILoginService _loginService;

    public LoginController(ILoginService loginService)
    {
        _loginService = loginService;
    }

    [HttpPost]
    [Route("username/{email}/password/{password}")]
    public async Task<IActionResult> Login([FromRoute] string email, [FromRoute] string password)
    {
        var result = await _loginService.Login(email, password);
        if (!result.IsSuccess)
            return StatusCode(result.Status, result.Error);
        
        Response.Cookies.Append("access_token", result.Value, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.UtcNow.AddHours(1)
        });

        return Ok(new
        {
            message = "Login successful"
        });
    }
    
}