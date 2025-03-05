using EComm.Entities;
using EComm.MvcUI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EComm.MvcUI.Controllers;

public class ProductController(IECommDb db) : Controller
{
    [HttpGet("product/{id}")]
    public async Task<IActionResult> Details(int id)
    {
        var product = await db.GetProduct(id);
        if (product is null)
        {
            return NotFound();
        }

        return View(product);
    }

    [HttpGet("product/edit/{id}")]
    public async Task<IActionResult> Edit(int id)
    {
        var product = await db.GetProduct(id, includeCategory: true);
        if (product is null)
        {
            return NotFound();
        }
        var categories = await db.GetAllCategories();

        var vm = new ProductEditViewModel();

        return View(vm);
    }

    [HttpPost("product/edit/{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Product product)
    {
        var existingProduct = await db.GetProduct(id);
        if (existingProduct is null)
        {
            return NotFound();
        }
        if (!ModelState.IsValid)
        {
            return View(product);
        }

        existingProduct.ProductName = product.ProductName;
        existingProduct.UnitPrice = product.UnitPrice;
        existingProduct.Package = product.Package;
        existingProduct.IsDiscontinued = product.IsDiscontinued;

        await db.UpdateProduct(existingProduct);

        return RedirectToAction("Details", new { id = id});
    }
}