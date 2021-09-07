using Application.Products.ViewModels;
using DataBase;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
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

        public async Task<Response> Do(ProductViewModel productview)
        {
            var product = new Product { Price = Convert.ToDouble(productview.Price), Name = productview.Name, Description = productview.Description };
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return new Response
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price
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
