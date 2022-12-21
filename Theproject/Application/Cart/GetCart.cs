using Application.Infrastructure;
using DataBase;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Application.Cart
{
    public class GetCart
    {
        private ApplicationDbContext _context;
        private ISessionManager _sessionManager;
        public GetCart(ISessionManager sessionManager, ApplicationDbContext context)
        {
            _sessionManager = sessionManager;
            _context = context;
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

            var cartList = _sessionManager.GetCart();

            if (cartList == null)
                return new List<Response>();

            var response = _context.Stocks
                .Include(x => x.Product)
                .AsEnumerable()
                .Where(x => cartList.Any(y => y.StockId == x.Id))
                .Select(x => new Response
                {

                    Name = x.Product.Name,
                    Value = $"$ {x.Product.Price:N2}",
                    StockId = x.Id,
                    RealValue = x.Product.Price,
                    Num = cartList.FirstOrDefault(y => y.StockId == x.Id).Num

                })
                .ToList();

            //todo appendding the cart
            return response;
        }
    }
}
