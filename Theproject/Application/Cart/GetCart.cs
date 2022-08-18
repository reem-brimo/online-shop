using Microsoft.AspNetCore.Http;
using Domain.Models;
using Newtonsoft.Json;
using DataBase;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Application.Cart
{
    public class GetCart
    {
        private ApplicationDbContext _context;
        private ISession _session;
        public GetCart(ISession session, ApplicationDbContext context)
        {
            _session = session;
            _context = context;
        }
        public class Response
        {
            public string Name { get; set; }
            public string Value { get; set; }
            public int StockId { get; set; }
            public int Num { get; set; }
        }
        public IEnumerable<Response> Do()
        {
            //todo: account for multipule  items in the cart
            var stringObject = _session.GetString("cart");

            if (string.IsNullOrEmpty(stringObject))
                return new List<Response>();

            var CartList = JsonConvert.DeserializeObject<List<CartProduct>>(stringObject);
            
            var response = _context.Stocks
                .Include(x => x.Product)
                .AsEnumerable()
                .Where(x => CartList.Any(y => y.StockId == x.Id))
                .Select(x => new Response {
                
                    Name = x.Product.Name,
                    Value = $"$ {x.Product.Price:N2}",
                    StockId = x.Id,
                    Num = CartList.FirstOrDefault(y => y.StockId == x.Id).Num
                
                })
                .ToList();

            //todo appendding the cart
            return response;
        }
    }
}
