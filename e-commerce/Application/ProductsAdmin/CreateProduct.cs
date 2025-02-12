﻿using Domain.Infrastructure;
using Domain.Models;
using System;
using System.Threading.Tasks;

namespace Application.ProductsAdmin
{
    [Service]
    public class CreateProduct
    {
        private readonly IProductManager _productManager;

        public CreateProduct(IProductManager productManager)
        {
            _productManager = productManager;
        }
        public async Task<Response> Do(ProductViewModel productview)
        {
            var product = new Product { 
                Price = Convert.ToDouble(productview.Price), 
                Name = productview.Name, 
                Description = productview.Description
            };
            if (await _productManager.CreateProduct(product) != 1)
            {
                //create custom execption
                throw new Exception("Failed to create product");
            }

            return new Response
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price.GetPriceString(),
            };
        }

        public class Response
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string Price { get; set; }
            public Response()
            {

            }

        }


        public class ProductViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string Price { get; set; }
        }

    }

}
