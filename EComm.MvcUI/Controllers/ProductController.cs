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

        var vm = new ProductEditViewModel()
        {
            Id = product.Id,
            ProductName = product.ProductName,
            Package = product.Package,
            IsDiscontinued = product.IsDiscontinued,
            UnitPrice = product.UnitPrice,
            CategoryId = product.CategoryId,
            Category = product.Category,
            Categories = categories
        };

        return View(vm);
    }

    [HttpPost("product/edit/{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, ProductEditViewModel vm)
    {
        var existingProduct = await db.GetProduct(id);
        if (existingProduct is null)
        {
            return NotFound();
        }
        if (!ModelState.IsValid)
        {
            vm.Categories = await db.GetAllCategories();
            return View(vm);
        }

        existingProduct.ProductName = vm.ProductName;
        existingProduct.CategoryId = vm.CategoryId;
        existingProduct.UnitPrice = vm.UnitPrice;
        existingProduct.Package = vm.Package;
        existingProduct.IsDiscontinued = vm.IsDiscontinued;

        await db.UpdateProduct(existingProduct);

        return RedirectToAction("Details", new { id = id});
    }
}