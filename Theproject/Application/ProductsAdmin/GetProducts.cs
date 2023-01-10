using Domain.Infrastructure;
using Domain.Models;
using System;
using System.Collections.Generic;

namespace Application.ProductsAdmin
{
    [Service]
    public class GetProducts
    {
        private readonly IProductManager _productManager;

        public GetProducts(IProductManager productManager)
        {
            _productManager = productManager;
        }

        public IEnumerable<ProductViewModel> Do() =>
            _productManager.GetProducts(Projection);
            

        private static Func<Product, ProductViewModel> Projection = (product) =>
        new ProductViewModel
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price.GetPriceString()
        };


    public class ProductViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string Price { get; set; }
        }


    }

}
