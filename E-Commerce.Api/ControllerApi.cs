using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Api;

public class ControllerApi : ControllerBase
{

    protected int GetUserId()
    {
        int userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        if (userId == null) throw new Exception("Error to get user id");
        return userId;
    }
}