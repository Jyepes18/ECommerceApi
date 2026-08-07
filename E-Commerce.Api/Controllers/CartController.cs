using ECommerce.Application.DTOs.Carts;
using ECommerce.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CartController : ControllerApi
{
    private readonly ICartService _cartService;

    public CartController(ICartService cartService)
    {
        _cartService = cartService;
    }
    
    [HttpPost]
    [Route("create")]
    public async Task<IActionResult> Insert(Cart cart)
    {
        var result = await _cartService.Insert(GetUserId(), cart);
        return Ok(result);
    }
    
    [HttpGet]
    [Route("get-all-cart-items")]
    public async Task<IActionResult> GetAllItems([FromQuery] int page, [FromQuery] int pageSize, [FromQuery] CartFilter cartFilter)
    {
        if (page < 0 || pageSize < 0) return BadRequest("Page or PageSize can´t be less that 0");
        
        var result = await _cartService.GetAllUserCartItems(GetUserId(), page, pageSize, cartFilter);
        return Ok(new { result.Value.Item1, result.Value.Total, result.Status });
    }
    
    [HttpDelete]
    [Route("delete-item/{itemId}")]
    public async Task<IActionResult> GetAllItems([FromRoute] int itemId)
    {
        return null;
    }
    

}