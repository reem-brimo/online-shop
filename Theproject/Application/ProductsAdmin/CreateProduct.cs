using Application.Products.ViewModels;
using DataBase;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.ProductsAdmin
{
    public class CreateProduct
    {
        private ApplicationDbContext _context;

        public CreateProduct(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Do(ProductViewModel product)
        {
            _context.Products.Add(new Product { Price = Convert.ToDouble(product.Price), Name = product.Name, Description = product.Description });
            await _context.SaveChangesAsync();
        }
    }
  
}
