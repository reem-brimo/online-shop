using Domain.Infrastructure;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.StocksAdmin
{
    [Service]
    public class GetStock
    {
        private readonly IProductManager _productManager;

        public GetStock(IProductManager productManager)
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
            Id = product.Id,
            Description = product.Description,
            Stock = product.Stock.Select(y => new StockViewModel
            {
                Id = y.Id,
                Description = y.Descripion,
                Num = y.Num
            })
        };

        public class StockViewModel
        {
            public int Id { get; set; }
            public int Num { get; set; }
            public string Description { get; set; }
  
        }

        public class ProductViewModel
        {
            public int Id { get; set; }
            public string Description { get; set; }
            public IEnumerable<StockViewModel> Stock{ get; set; }
        }
    }
}
