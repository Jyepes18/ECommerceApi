using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Api;

public class ControllerApi : ControllerBase
{

    protected int GetUserId()
    {
        var calim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!int.TryParse(calim, out var userId)) throw new Exception("Error to get user id");
        
        return userId;
    }
}