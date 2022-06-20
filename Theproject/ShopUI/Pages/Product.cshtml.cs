using DataBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Application.Products;
using Microsoft.AspNetCore.Http;
using Application.Cart;

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
        public IActionResult OnGet(string name)
        {
            Product = new GetProduct(_context).Do(name.Replace("-", " "));
            if (Product == null)
                return RedirectToPage("index");
            else
                return Page();
        }

        public IActionResult OnPost()
        {
            new AddToCart(HttpContext.Session).Do(CartViewModel);
            return RedirectToPage("Cart");
        }
    }
}
