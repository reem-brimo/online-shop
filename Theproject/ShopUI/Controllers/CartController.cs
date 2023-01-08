using Application.Cart;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace ShopUI.Controllers
{
    [Route("[Controller]/[action]")]
    public class CartController : Controller
    {

        [HttpPost("{stockId}")]
        public async Task<IActionResult> AddOne(
            int stockId,
            [FromServices] AddToCart addToCart)
        {
            var request = new AddToCart.Request
            {
                StockId = stockId,
                Num = 1
            };

            var success = await addToCart.Do(request);

            if (success)
                return Ok("Item Added to cart");

            return BadRequest("Failed to add to cart");
        }

        [HttpPost("{stockId}/{num}")]
        public async Task<IActionResult> Remove(
            int stockId,
            int num,
            [FromServices] RemoveFromCart removeFromCart)
        {
            var request = new RemoveFromCart.Request
            {
                StockId = stockId,
                Num = num
            };

            var success = await removeFromCart.Do(request);


            if (success)
                return Ok("Item removed from cart");

            return BadRequest("Failed to remove item from cart");
        }

        [HttpGet]
        public IActionResult GetCartComponent([FromServices] GetCart getCart)
        {
            var result = getCart.Do().Sum(x => x.RealValue * x.Num);
            return PartialView("Components/Cart/Small", $"${result}");
        }

        [HttpGet]
        public IActionResult GetCartMain([FromServices] GetCart getCart)
        {
            var cart = getCart.Do();
            return PartialView("_CartPartial", cart);
        }
       
    }
}
