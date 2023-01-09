using DataBase;
using Domain.Infrastructure;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Products
{
    public class GetProduct
    {
        private readonly IProductManager _productManager;
        public IStockManager _stockManager { get; }


        public GetProduct(IProductManager productManager, IStockManager stockManager)
        {
            _productManager = productManager;
            _stockManager = stockManager;
        }


        public async Task<ProductViewModel> Do(string name)
        {

           await _stockManager.RetriveExpiredStockOnHold();


            return _productManager.GetProductByName(name, Projection);
             
        }

        private static Func<Product, ProductViewModel> Projection = (product) =>
        new ProductViewModel
        {
            Name = product.Name,
            Description = product.Description,
            Price = product.Price.GetPriceString(),
            Stock = product.Stock.Select(y => new StockViewModel
            {
                Id = y.Id,
                Descripion = y.Descripion,
                Num = y.Num
            })
        };



        public class ProductViewModel
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public string Price { get; set; }
            public IEnumerable<StockViewModel> Stock { get; set; }
        }

        public class StockViewModel
        {
            public int Id { get; set; }
            public string Descripion { get; set; }
            public int Num { get; set; }
        }

    }

}
