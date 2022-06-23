using DataBase;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Products
{
    public class GetProducts
    {
        private ApplicationDbContext _context;

        public GetProducts(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ProductViewModel>  Do( ) => _context.Products.ToList().Select(x =>
           new ProductViewModel
           {
               Name = x.Name,
               Description = x.Description,
               Price = $"$ {x.Price.ToString("N2")}" 
           });

        public class ProductViewModel
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public string Price { get; set; }
        }
    }


}
