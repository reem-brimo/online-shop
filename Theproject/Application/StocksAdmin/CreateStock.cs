using Application.Products.ViewModels;
using DataBase;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.StocksAdmin
{
    public  class CreateStock
    {
        private readonly ApplicationDbContext _context;

        public CreateStock(ApplicationDbContext context)
        {
            _context = context;

        }

        public async Task<Response> Do(Request stockView)
        {
            var stock = new Stock
            {
                Descripion = stockView.Descripion,
                Num = stockView.Num,
                ProductId = stockView.ProductId
            };

            _context.Stocks.Add(stock);
            await _context.SaveChangesAsync();

            return new Response
            {
                Id = stock.Id,
                Num = stock.Num,
                Descripion = stock.Descripion
            };
        }

      

        public class Response
        {
            public int Id { get; set; }
            public int Num { get; set; }
            public string Descripion { get; set; }
        }

        public class Request
        {
            public int ProductId { get; set; }
            public string Descripion { get; set; }
            public int Num { get; set; }
        }

    }
    

  
}
