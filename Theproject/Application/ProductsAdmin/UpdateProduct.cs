using Application.Products.ViewModels;
using DataBase;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
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

        public async Task<Response> Do(ProductViewModel product)
        {
            var productModel = await _context.Products.FirstOrDefaultAsync(x => x.Id == product.Id);

            productModel.Name = product.Name;
            productModel.Description = product.Description;
            productModel.Price = Convert.ToDouble(product.Price);

            await _context.SaveChangesAsync();
            return new Response
            {
                Id = productModel.Id,
                Name = productModel.Name,
                Description = productModel.Description,
                Price = productModel.Price,

            };

        }

      

        public class Response
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public double Price { get; set; }
            public Response()
            {

            }

        }
    }
  
}
