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
    public class RemoveFromCart
    {
        private ApplicationDbContext _context;
        private ISession _session;
        public RemoveFromCart(ISession session, ApplicationDbContext context)
        {
            _context = context;
            _session = session;
        }
        public class Request
        {
            public int StockId { get; set; }
            public int Num { get; set; }
            public bool All { get; set; }

        }
        public async Task<bool> Do(Request request)
        {

            //edit num in cartproduct of the session
            var cartList = new List<CartProduct>();
            var stringObject = _session.GetString("cart");

            if (string.IsNullOrEmpty(stringObject))
            {
                return true;
            }
            cartList = JsonConvert.DeserializeObject<List<CartProduct>>(stringObject);

            if (!cartList.Any(x => x.StockId == request.StockId))
            {
                return true;
            }

            var product = cartList.Find(x => x.StockId == request.StockId);

            if (!request.All)
                product.Num -= request.Num;

            else
                product.Num = 0;

            if (product.Num <= 0)
                cartList.Remove(product);

            stringObject = JsonConvert.SerializeObject(cartList);

            _session.SetString("cart", stringObject);


            //edit num in stock on hold  or remove record

            var stockOnHold = _context.StocksOnHold
                .FirstOrDefault(x => x.StockId == request.StockId
                                && x.SessionId == _session.Id);

            if (stockOnHold != null)
            {
                var stock = _context.Stocks.FirstOrDefault(x => x.Id == request.StockId);

                if (request.All)
                {
                    stock.Num += stockOnHold.Num;
                    stockOnHold.Num = 0;
                }
                else
                {
                    stock.Num += request.Num;
                    stockOnHold.Num -= request.Num;
                }


                if (stockOnHold.Num <= 0)
                {
                    _context.Remove(stockOnHold);
                }
                await _context.SaveChangesAsync();
            }

            else
            {
                //error
                return false;
            }

            return true;
        }
    }
}
