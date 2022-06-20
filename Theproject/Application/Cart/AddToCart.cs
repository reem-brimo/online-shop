using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace Application.Cart
{
    public class AddToCart
    {
        private ISession _session;
        public AddToCart(ISession session)
        {
            _session = session;
        }
        public class Request
        {
            public int StockId { get; set; }
            public int Num { get; set; }
        }
        public void Do(Request request)
        {
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
        }
    }
}
