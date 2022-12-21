using Application.Cart;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ShopUI.ViewComponents
{
    public class CartViewComponent : ViewComponent
    {
        private GetCart _getCart;
        public CartViewComponent(GetCart getCart)
        {
            _getCart = getCart;
        }

        public IViewComponentResult Invoke(string view = "Default")
        {
            if (view == "Small")
            {
                var result = _getCart.Do().Sum(x => x.RealValue * x.Num);
                return View(view, $"${result}");

            }
            return View(view, _getCart.Do());
        }

    }
}
