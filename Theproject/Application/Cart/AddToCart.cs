using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Domain.Models;

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
            var CartProduct = new CartProduct
            {
                StockId = request.StockId,
                Num = request.Num,
            };
            var stringObject = JsonConvert.SerializeObject(request);
            //todo appendding the cart
            _session.SetString("cart", stringObject);
        }
    }
}
