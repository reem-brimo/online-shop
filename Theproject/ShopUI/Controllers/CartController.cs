using Application.Cart;
using DataBase;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopUI.Controllers
{   
    [Route("[Controller]/[action]")]
    public class CartController : Controller
    {
        private ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("{stockId}")]
        public async Task<IActionResult> AddOne(int stockId)
        {
            var request = new AddToCart.Request
            {
                StockId = stockId,
                Num = 1
            };

            var addToCart = new AddToCart(HttpContext.Session, _context);

            var success = await addToCart.Do(request);


            if(success)
                return Ok("Item Added to cart");

            return BadRequest("Failed to add to cart");
        }
        
        [HttpPost("{stockId}")]
        public async Task<IActionResult> RemoveOne(int stockId)
        {
            var request = new RemoveFromCart.Request
            {
                StockId = stockId,
                Num = 1
            };

            var removeFromCart = new RemoveFromCart(HttpContext.Session, _context);

            var success = await removeFromCart.Do(request);


            if(success)
                return Ok("Item removed from cart");

            return BadRequest("Failed to remove item from cart");
        }
        //same path 
        [HttpPost("{stockId}")]
        public async Task<IActionResult> RemoveAll(int stockId)
        {
            var request = new RemoveFromCart.Request
            {
                StockId = stockId,
                All = true
            };

            var removeFromCart = new RemoveFromCart(HttpContext.Session, _context);

            var success = await removeFromCart.Do(request);


            if(success)
                return Ok("All Items removed from cart");

            return BadRequest("Failed to remove all items from cart");
        } 
    }
}
