using System.Diagnostics;
using EComm.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EComm.MvcUI.Models;

public class HomeController(IECommDb db) : Controller
{
    public async Task<IActionResult> Index()
    {
        var r = await db.GetAllProducts();

        return View(r);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
