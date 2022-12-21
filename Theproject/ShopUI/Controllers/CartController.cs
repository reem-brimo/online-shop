using Application.Cart;
using Microsoft.AspNetCore.Mvc;
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

        [HttpPost("{stockId}")]
        public async Task<IActionResult> RemoveOne(
            int stockId,
            [FromServices] RemoveFromCart removeFromCart)
        {
            var request = new RemoveFromCart.Request
            {
                StockId = stockId,
                Num = 1
            };

            var success = await removeFromCart.Do(request);


            if (success)
                return Ok("Item removed from cart");

            return BadRequest("Failed to remove item from cart");
        }
        //same path 
        [HttpPost("{stockId}")]
        public async Task<IActionResult> RemoveAll(
            int stockId,
            [FromServices] RemoveFromCart removeFromCart)
        {
            var request = new RemoveFromCart.Request
            {
                StockId = stockId,
                All = true
            };

            var success = await removeFromCart.Do(request);


            if (success)
                return Ok("All Items removed from cart");

            return BadRequest("Failed to remove all items from cart");
        }
    }
}
