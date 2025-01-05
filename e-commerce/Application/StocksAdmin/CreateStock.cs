using Domain.Infrastructure;
using Domain.Models;
using System.Threading.Tasks;

namespace Application.StocksAdmin
{
    [Service]
    public  class CreateStock
    {
        private readonly IStockManager _stockManager;

        public CreateStock(IStockManager stockManager)
        {
            _stockManager = stockManager;
        }

        public async Task<Response> Do(Request stockView)
        {
            var stock = new Stock
            {
                Descripion = stockView.Description,
                Num = stockView.Num,
                ProductId = stockView.ProductId
            };

            await _stockManager.CreateStock(stock);

            return new Response
            {
                Id = stock.Id,
                Num = stock.Num,
                Description = stock.Descripion
            };
        }

      

        public class Response
        {
            public int Id { get; set; }
            public int Num { get; set; }
            public string Description { get; set; }
        }

        public class Request
        {
            public int ProductId { get; set; }
            public string Description { get; set; }
            public int Num { get; set; }
        }

    }
    

  
}
