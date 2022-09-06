using DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Products
{
    public class GetProducts
    {
        private ApplicationDbContext _context;

        public GetProducts(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ProductViewModel>  Do( ) => 
            _context.Products
                    .Include(x => x.Stock)
                    .Select(x =>
                   new ProductViewModel
                   {
                       Name = x.Name,
                       Description = x.Description,
                       Price = $"$ {x.Price.ToString("N2")}" ,
                       StockCount = x.Stock.Sum(y=> y.Num)
                   })
                    .ToList();

        public class ProductViewModel
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public string Price { get; set; }
            public int StockCount{ get; set; }
        }
    }


}
