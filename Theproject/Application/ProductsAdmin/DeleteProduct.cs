using Application.Products;
using DataBase;
using Domain.Infrastructure;
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
        private readonly IProductManager _productManager;

        public DeleteProduct(IProductManager productManager)
        {
            _productManager = productManager;
        }

        public async Task<bool> Do(int id)
        {
            if(await _productManager.DeleteProduct(id) > 0)
            {
                //create custom execption
                throw new Exception("Failed to Delete product");
            }
            
            return true;
        }
    }
  
}
