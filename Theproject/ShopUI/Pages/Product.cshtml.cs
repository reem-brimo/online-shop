using Application.Cart;
using Application.Products;
using DataBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace ShopUI.Pages
{
    public class ProductModel : PageModel
    {
        private ApplicationDbContext _context;

        public ProductModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public AddToCart.Request CartViewModel { get; set; }

        public GetProduct.ProductViewModel Product { get; set; }
        public async Task<IActionResult> OnGet(string name)
        {
            Product = await new GetProduct(_context).Do(name.Replace("-", " "));
            if (Product == null)
                return RedirectToPage("index");

            return Page();
        }

        public async Task<IActionResult> OnPost([FromServices] AddToCart addToCart)
        {
            var AddedToCart = await addToCart.Do(CartViewModel);

            if (AddedToCart)
                return RedirectToPage("Cart");

            return Page();
        }
    }
}
