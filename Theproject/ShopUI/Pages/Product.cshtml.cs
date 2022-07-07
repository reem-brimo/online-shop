using DataBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Application.Products;
using Microsoft.AspNetCore.Http;
using Application.Cart;
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

        public async Task<IActionResult> OnPost()
        {
            var AddedToCart = await new AddToCart(HttpContext.Session, _context).Do(CartViewModel);

            if(AddedToCart)
            return RedirectToPage("Cart");

            return Page();
        }
    }
}
