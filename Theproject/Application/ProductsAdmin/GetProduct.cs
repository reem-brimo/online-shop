using Application.Products.ViewModels;
using DataBase;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ProductsAdmin
{
    public class GetProduct
    {
        private ApplicationDbContext _context;

        public GetProduct(ApplicationDbContext context)
        {
            _context = context;
        }

        public ProductViewModel  Do(int id ) => _context.Products.Where(x=> x.Id == id).Select(x =>
           new ProductViewModel
           {
               Id = x.Id,
               Name = x.Name,
               Description = x.Description,
               Price = $"{x.Price}" 
           }).FirstOrDefault();
        
    }
  
}
