using EComm.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EComm.MvcUI.Controllers
{
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
    }
}
