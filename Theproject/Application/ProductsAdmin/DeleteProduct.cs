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
    public class DeleteProduct
    {
        private ApplicationDbContext _context;

        public DeleteProduct(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Do(int id)
        {
            var product = _context.Products.FirstOrDefault(x => x.Id == id);
            _context.Products.Remove(product);

            await _context.SaveChangesAsync();
        }
    }
  
}
