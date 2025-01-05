using Domain.Infrastructure;
using System.Collections.Generic;

namespace Application.Cart
{
    [Service]
    public class GetCart
    {
        private ISessionManager _sessionManager;
        public GetCart(ISessionManager sessionManager )
        {
            _sessionManager = sessionManager;
        }
        public class Response
        {
            public string Name { get; set; }
            public string Value { get; set; }
            public double RealValue { get; set; }
            public int StockId { get; set; }
            public int Num { get; set; }
        }
        public IEnumerable<Response> Do()
        {
            //todo: account for multipule  items in the cart
            return _sessionManager
                .GetCart(x => new Response
                {

                    Name = x.ProductName,
                    Value = x.Price.GetPriceString(),
                    StockId = x.StockId,
                    RealValue = x.Price,
                    Num = x.Num

                });
        }
    }
}
