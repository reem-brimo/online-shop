using Application.Products.ViewModels;
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
               Descripion = x.Descripion,
               Price = $"$ {x.Price.ToString("N2")}" 
           });
        
    }
  
}
