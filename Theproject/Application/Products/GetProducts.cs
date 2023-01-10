using Domain.Infrastructure;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Products
{
    [Service]
    public class GetProducts
    {
        private readonly IProductManager _productManager;

        public GetProducts(IProductManager productManager)
        {
            _productManager = productManager;
        }

        public IEnumerable<ProductViewModel> Do()
        {
            return _productManager.GetProducts(Projection);
        }

        private static Func<Product, ProductViewModel> Projection = (product) =>
           new ProductViewModel
           {
               Name = product.Name,
               Description = product.Description,
               Price = product.Price.GetPriceString(),
               StockCount = product.Stock.Sum(y => y.Num)
           };



        public class ProductViewModel
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public string Price { get; set; }
            public int StockCount{ get; set; }
        }
    }


}
