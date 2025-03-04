using EComm.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EComm.MvcUI.ViewComponents
{
    public class ProductList(IECommDb db) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var products = await db.GetAllProducts();
            return View(products);
        }
    }
}
