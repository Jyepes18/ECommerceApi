using ECommerce.Application.DTOs.Products;
using ECommerce.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerApi
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }
    
    [HttpPost]
    [Authorize(Roles = "ADMIN")]
    [Route("create")]
    public async Task<IActionResult> Create([FromBody] Product product)
    {
        var result = await _productService.Create(product, GetUserId());
        return Ok(result);
    }

    [HttpGet]
    [Authorize(Roles = "ADMIN")]
    [Route("get-all-products-by-user")]
    public async Task<IActionResult> GetAllByUser([FromQuery] int page, [FromQuery] int pageSize, [FromQuery] ProductFilter productFilter)
    {

        if (page < 0 || pageSize < 0) return BadRequest("Page or PageSize can´t be less that 0");
        
        var result = await _productService.GetAllByUser(GetUserId(), page, pageSize, productFilter);
        return Ok(new { result.Value.Item1, result.Value.Total, result.Status });
    }

    [HttpPut]
    [Authorize(Roles = "ADMIN")]
    [Route("update/{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, Product product)
    {
        var result = await _productService.Update(id, product, GetUserId());
        return Ok(result);
    }
    
    [HttpDelete]
    [Authorize(Roles = "ADMIN")]
    [Route("delete/{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var result = await _productService.Delete(id, GetUserId());
        return Ok(result);
    }
    
    [HttpGet]
    [Authorize(Roles = "USER")]
    [Route("get-all-products")]
    public async Task<IActionResult> GetAll([FromQuery] int page, [FromQuery] int pageSize, [FromQuery] ProductFilter productFilter)
    {

        if (page < 0 || pageSize < 0) return BadRequest("Page or PageSize can´t be less that 0");
        
        var result = await _productService.GetAll(GetUserId(), page, pageSize, productFilter);
        return Ok(new { result.Value.Item1, result.Value.Total, result.Status });
    }
}