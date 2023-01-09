using Domain.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain.Infrastructure;

namespace DataBase
{
    public class StockManager : IStockManager
    {
        private ApplicationDbContext _context;

        public StockManager(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool EnoughStock(int stockId, int num)
        {
            return _context.Stocks.FirstOrDefault(x => x.Id == stockId).Num >= num;
        }

        public Stock GetStock(int stockId)
        {
            return _context.Stocks
           .Include(x => x.Product)
           .FirstOrDefault(x => x.Id == stockId);
        }

        public Task PutStockOnHold(int stockId, int num, string sessionId)
        {

            //database responsibility update the stock

            _context.Stocks.FirstOrDefault(x => x.Id == stockId).Num -= num;


            var stockOnHold = _context.StocksOnHold
                .Where(x => x.SessionId == sessionId)
                .ToList();

            if (stockOnHold.Any(x => x.StockId == stockId))
            {
                stockOnHold.Find(x => x.StockId == stockId).Num += num;
            }

            else
            {
                _context.StocksOnHold.Add(new StockOnHold
                {
                    StockId = stockId,
                    SessionId = sessionId,
                    Num = num,
                    Expiration = DateTime.Now.AddMinutes(20)
                });
            }


            foreach (var stock in stockOnHold)
            {
                stock.Expiration = DateTime.Now.AddMinutes(20);
            }

            return _context.SaveChangesAsync();
        }

        public Task RetriveExpiredStockOnHold()
        {
            var stocksOnHold = _context.StocksOnHold.Where(x => x.Expiration < DateTime.Now).ToList();

            if (stocksOnHold.Count > 0)
            {
                var stockToReturn = _context.Stocks
                                            .Where(x => stocksOnHold.Select(y => y.StockId).Contains(x.Id))
                                            .ToList();

                foreach (var stock in stockToReturn)
                {
                    stock.Num += stocksOnHold.FirstOrDefault(x => x.StockId == stock.Id).Num;
                }
                _context.StocksOnHold.RemoveRange(stocksOnHold);

                return _context.SaveChangesAsync();
            }
            return Task.CompletedTask;

        }

        public Task RemoveStockFromHold(string sessionId)
        {

            var stocksOnHold = _context.StocksOnHold
                          .Where(x => x.SessionId == sessionId)
                          .ToList();

            _context.StocksOnHold.RemoveRange(stocksOnHold);

            return _context.SaveChangesAsync();
        }

        public Task RemoveStockFromHold(int stockId, int num, string sessionId)
        {
            var stockOnHold = _context.StocksOnHold
            .FirstOrDefault(x => x.StockId == stockId
                            && x.SessionId == sessionId);

            var stock = _context.Stocks
                .FirstOrDefault(x => x.Id == stockId);

            stock.Num += num;
            stockOnHold.Num -= num;


            if (stockOnHold.Num <= 0)
            {
                _context.Remove(stockOnHold);
            }
            return _context.SaveChangesAsync();

        }

 
    }

}
