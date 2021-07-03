using Application.Products.ViewModels;
using DataBase;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.ProductsAdmin
{
    public class UpdateProduct
    {
        private ApplicationDbContext _context;

        public UpdateProduct(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Do(ProductViewModel product)
        {
            await _context.SaveChangesAsync();
        }
    }
  
}
