﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EComm.API.Controllers;

[ApiController]
public class ProductController(ILogger<ProductController> logger, IECommDb db) : ControllerBase
{
    [HttpGet("/products")]
    //[Authorize(Policy = "AdminsOnly")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<Product>>> GetAllProducts(int start = 0)
    {
        if (start == 0)
        {
            return (await db.GetAllProducts()).ToList();
        }
        return (await db.GetAllProductsByPage(start)).ToList();
    }

    [HttpGet("/products/search")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<List<Product>>> ProductSearch(string q = "")
    {
        if (q.Length > 10) return BadRequest(new { Reason = "Query too long"});

        var products = db.DefProducts;

        var dr = products.Where(p => p.ProductName.StartsWith(q));

        var r = await db.EnumerateProducts(dr);

        return Ok(r);
    }

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
            logger.LogInformation("Product {name} deleted", existingProduct.ProductName);
            return NoContent();
        }
    }
}
