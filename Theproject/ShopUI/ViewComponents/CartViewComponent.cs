using Microsoft.AspNetCore.Mvc;
using DataBase;
using Application.Cart;

namespace ShopUI.ViewComponents
{
    public class CartViewComponent : ViewComponent
    {
        private ApplicationDbContext _context;
        public CartViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke(string view = "Default")
        {
            return View(view, new GetCart(HttpContext.Session, _context).Do());
        }

    }
}
