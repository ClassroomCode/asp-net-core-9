using EComm.Entities;
using EComm.MvcUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
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
    [Authorize(Policy = "AdminsOnly")]
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
    [Authorize(Policy = "AdminsOnly")]
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

    [HttpPost("product/addtocart")]
    public async Task<IActionResult> AddToCart(int id, int quantity)
    {
        var product = await db.GetProduct(id);
        if (product == null) return NotFound();
        var totalCost = quantity * product.UnitPrice;

        string message = $"You added {product.ProductName} " +
                         $"(x {quantity}) to your cart " +
                         $"at a total cost of {totalCost:C}.";

        var cart = ShoppingCart.GetFromSession(HttpContext.Session);
        var lineItem = cart.LineItems.SingleOrDefault(item => item.Product.Id == id);
        if (lineItem != null)
        {
            lineItem.Quantity += quantity;
        }
        else
        {
            cart.LineItems.Add(new ShoppingCart.LineItem
            {
                Product = product,
                Quantity = quantity
            });
        }
        ShoppingCart.StoreInSession(cart, HttpContext.Session);

        return PartialView("_AddedToCart", message);
    }

    [HttpGet("product/cart")]
    public IActionResult Cart()
    {
        var cart = ShoppingCart.GetFromSession(HttpContext.Session);
        return View(cart);
    }
}