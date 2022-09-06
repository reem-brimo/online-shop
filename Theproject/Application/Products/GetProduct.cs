using DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Products
{
    public class GetProduct
    {
        private ApplicationDbContext _context;

        public GetProduct(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ProductViewModel> Do(string name)
        {

           var stocksOnHold = _context.StocksOnHold.Where(x => x.Expiration < DateTime.Now).ToList();

            if(stocksOnHold.Count > 0)
            {
                var stockToReturn = _context.Stocks
                                            .Where(x => stocksOnHold.Select(y => y.StockId).Contains(x.Id))
                                            .ToList();

                foreach (var stock in stockToReturn)
                {
                    stock.Num += stocksOnHold.FirstOrDefault(x => x.StockId == stock.Id).Num;
                }
                _context.StocksOnHold.RemoveRange(stocksOnHold);

                await _context.SaveChangesAsync();
            }

           return  _context.Products
                            .Include(x => x.Stock)
                            .Where(x => x.Name == name)
                            .Select(x => new ProductViewModel
                            {
                                Name = x.Name,
                                Description = x.Description,
                                Price = $"$ {x.Price.ToString("N2")}",
                                Stock = x.Stock.Select(y => new StockViewModel
                                {
                                    Id = y.Id,
                                    Descripion = y.Descripion,
                                    Num = y.Num
                                })
                            })
                            .FirstOrDefault();
        }

        public class ProductViewModel
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public string Price { get; set; }
            public IEnumerable<StockViewModel> Stock { get; set; }
        }

        public class StockViewModel
        {
            public int Id { get; set; }
            public string Descripion { get; set; }
            public int Num { get; set; }
        }

    }

}
