using Microsoft.AspNetCore.Http;
using Domain.Models;
using Newtonsoft.Json;
using DataBase;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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
        public Response Do()
        {
            //todo: account for multipule  items in the cart
            var stringObject = _session.GetString("cart");

            var CartProduct = JsonConvert.DeserializeObject<CartProduct>(stringObject);

            var response = _context.Stocks
                .Include(x => x.Product)
                .Where(x => x.Id == CartProduct.StockId)
                .Select(x => new Response {
                 
                    Name = x.Product.Name,
                    Value = $"$ {x.Product.Price.ToString("N2")}",
                    StockId = x.Id,
                    Num = x.Num
                
                }).FirstOrDefault();

            //todo appendding the cart
            return response;
        }
    }
}
