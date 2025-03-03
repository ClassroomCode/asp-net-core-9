using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EComm.RazorUI.Pages
{
    public class IndexModel(IECommDb db) : PageModel
    {
        public IEnumerable<string> ProductNames { get; private set; } = [];

        public async Task OnGetAsync()
        {
            var products = await db.GetAllProducts();

            ProductNames = products
                .OrderBy(p => p.ProductName)
                .Select(p => p.ProductName).ToArray();
        }
    }
}
