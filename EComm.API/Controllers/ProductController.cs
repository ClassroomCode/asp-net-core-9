using Microsoft.AspNetCore.Mvc;

namespace EComm.API.Controllers;

//[ApiController]
public class ProductController(IECommDb db) : ControllerBase
{
    [HttpGet("/products")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<Product>>> GetAllProducts() =>
        (await db.GetAllProducts()).ToList();

    [HttpGet("/products/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await db.GetProduct(id);
        return product is null ? NotFound() : Ok(product);
    }
}
