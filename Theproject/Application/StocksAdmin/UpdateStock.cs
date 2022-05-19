using DataBase;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.StocksAdmin
{
    public class UpdateStock
    {
        private ApplicationDbContext _context;

        public UpdateStock(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Response> Do(Request stocksView)
        {
            var stocks = new List<Stock>();

            foreach (var item in stocksView.Stock)
            {
                stocks.Add(new Stock
                {
                    Id = item.Id,
                    Descripion = item.Descripion,
                    Num = item.Num,
                    ProductId = item.ProductId
                });
            }

            _context.Stocks.UpdateRange(stocks);
            await _context.SaveChangesAsync();

            return new Response
            {
                Stock = stocksView.Stock
            };
        }

        public class StockViewModel
        {
            public int Id { get; set; }
            public int Num { get; set; }
            public string Descripion { get; set; }
            public int ProductId { get; set; }

        }

        public class Response
        {
            public IEnumerable<StockViewModel> Stock { get; set; }

        }

        public class Request
        {
            public IEnumerable<StockViewModel> Stock { get; set; }
        }


    }
}
