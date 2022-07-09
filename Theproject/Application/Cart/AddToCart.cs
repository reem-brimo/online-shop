using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System;
using DataBase;
using System.Threading.Tasks;

namespace Application.Cart
{
    public class AddToCart
    {
        private ApplicationDbContext _context;
        private ISession _session;
        public AddToCart(ISession session, ApplicationDbContext context)
        {
            _context = context;
            _session = session;
        }
        public class Request
        {
            public int StockId { get; set; }
            public int Num { get; set; }
        }
        public async Task<bool> Do(Request request)
        {

            //adding filters for handling update expration date
            var stockOnHold = _context.StocksOnHold.Where(x => x.SessionId == _session.Id).ToList();

            var stockToHold = _context.Stocks.Where(x => x.Id == request.StockId).FirstOrDefault();

            if(stockToHold.Num < request.Num)
            {
                return false;
               // return NotFound("");
            }

            _context.StocksOnHold.Add(new StockOnHold
            {
                StockId = stockToHold.Id,
                SessionId = _session.Id,
                Num = request.Num,
                Expiration = DateTime.Now.AddMinutes(20)
            });

            stockToHold.Num -= request.Num;

            foreach (var stock in stockOnHold)
            {
                stock.Expiration = DateTime.Now.AddMinutes(20);
            }

            await _context.SaveChangesAsync();

            var cartList = new List<CartProduct>();
            var stringObject = _session.GetString("cart");

            if (!string.IsNullOrEmpty(stringObject))
            {
                cartList = JsonConvert.DeserializeObject<List<CartProduct>>(stringObject);
            }
            if (cartList.Any(x => x.StockId == request.StockId))
            {
                cartList.Find(x => x.StockId == request.StockId).Num += request.Num;
            }
            else
            {
                cartList.Add( new CartProduct
                {
                    StockId = request.StockId,
                    Num = request.Num,
                });

            }
            stringObject = JsonConvert.SerializeObject(cartList);
            _session.SetString("cart", stringObject);

            return true;
        }
    }
}
