using Application.Infrastructure;
using DataBase;
using Domain.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Cart
{
    public class AddToCart
    {
        private ApplicationDbContext _context;
        private ISessionManager _sessionManager;

        public AddToCart(ISessionManager sessionManager, ApplicationDbContext context)
        {
            _context = context;
            _sessionManager = sessionManager;
        }
        public class Request
        {
            public int StockId { get; set; }
            public int Num { get; set; }
        }
        public async Task<bool> Do(Request request)
        {

            //adding filters for handling update expration date
            var stockOnHold = _context.StocksOnHold.Where(x => x.SessionId == _sessionManager.GetId()).ToList();

            var stockToHold = _context.Stocks.Where(x => x.Id == request.StockId).FirstOrDefault();

            if (stockToHold.Num < request.Num)
            {
                return false;
                // return NotFound("");
            }

            if (stockOnHold.Any(x => x.StockId == request.StockId))
            {
                stockOnHold.Find(x => x.StockId == request.StockId).Num += request.Num;
            }

            else
            {
                _context.StocksOnHold.Add(new StockOnHold
                {
                    StockId = stockToHold.Id,
                    SessionId = _sessionManager.GetId(),
                    Num = request.Num,
                    Expiration = DateTime.Now.AddMinutes(20)
                });
            }

            stockToHold.Num -= request.Num;

            foreach (var stock in stockOnHold)
            {
                stock.Expiration = DateTime.Now.AddMinutes(20);
            }

            await _context.SaveChangesAsync();

            _sessionManager.AddProduct(request.StockId, request.Num);

            return true;
        }
    }
}
