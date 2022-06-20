using Application.Cart;
using DataBase;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ShopUI.Pages
{
    public class CartModel : PageModel
    {

        private ApplicationDbContext _context;
        public CartModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public GetCart.Response Cart { get; set; }

        public IActionResult OnGet()
        {
            Cart = new GetCart(HttpContext.Session, _context).Do();

            return Page();
        }
    }
}
