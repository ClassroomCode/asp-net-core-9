using Microsoft.AspNetCore.Mvc;

namespace EComm.API.Controllers;

[ApiController]
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

    [HttpPost("/products")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> CreateProduct(Product product)
    {
        if (product.ProductName.StartsWith("z"))
        {
            ModelState.AddModelError("name", "Need a name!");
        }
        if (!ModelState.IsValid)
        {
            return ValidationProblem();
        }

        await db.AddProduct(product);
        return CreatedAtAction("GetProduct", new { id = product.Id }, product);
    }

    [HttpPut("/products/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult> PutProduct(int id, Product product)
    {
        var existingProduct = await db.GetProduct(id);
        if (existingProduct is null)
        {
            await db.AddProduct(product);
            return CreatedAtAction("GetTodo", new { id = product.Id }, product);
        }
        else
        {
            existingProduct.ProductName = product.ProductName;
            existingProduct.UnitPrice = product.UnitPrice;
            existingProduct.Package = product.Package;
            existingProduct.IsDiscontinued = product.IsDiscontinued;
            await db.UpdateProduct(existingProduct);
            return NoContent();
        }
    }

    [HttpDelete("/products/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        var existingProduct = await db.GetProduct(id);
        if (existingProduct is null)
        {
            return NotFound();
        }
        else
        {
            await db.DeleteProduct(existingProduct);
            return NoContent();
        }
    }
}
