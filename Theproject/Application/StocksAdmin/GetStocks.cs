using Application.Products.ViewModels;
using DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.StocksAdmin
{
   public class GetStocks
    {
        private ApplicationDbContext _context;

        public GetStocks(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Response> Do(int productId)
        {
            

           var stocks = _context.Stocks.Where(x => x.ProductId == productId).Select( x => new Response { 
           Id = x.Id,
           Descripion = x.Descripion,
           Num = x.Num          
           }).ToList();

            return stocks;
        }

        public class Response
        {
            public int Id { get; set; }
            public int Num { get; set; }
            public string Descripion { get; set; }
        }
    }
}
