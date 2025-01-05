using Domain.Infrastructure;
using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.StocksAdmin
{
    [Service]
    public class UpdateStock
    {
        private readonly IStockManager _stockManager;

        public UpdateStock(IStockManager stockManager)
        {
            _stockManager = stockManager;
        }

        public async Task<Response> Do(Request stocksView)
        {
            var stocks = new List<Stock>();

            foreach (var item in stocksView.Stock)
            {
                stocks.Add(new Stock
                {
                    Id = item.Id,
                    Descripion = item.Description,
                    Num = item.Num,
                    ProductId = item.ProductId
                });
            }

            await _stockManager.UpdateStock(stocks);

            return new Response
            {
                Stock = stocksView.Stock
            };
        }

        public class StockViewModel
        {
            public int Id { get; set; }
            public int Num { get; set; }
            public string Description { get; set; }
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
