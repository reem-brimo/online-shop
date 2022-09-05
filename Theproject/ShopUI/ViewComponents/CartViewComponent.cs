using Microsoft.AspNetCore.Mvc;
using DataBase;
using Application.Cart;
using System.Linq;

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
            if (view == "Small")
            {
                var result = new GetCart(HttpContext.Session, _context).Do().Sum(x => x.RealValue * x.Num);
                return View(view, $"${result}");

            }
            return View(view, new GetCart(HttpContext.Session, _context).Do());
        }

    }
}
