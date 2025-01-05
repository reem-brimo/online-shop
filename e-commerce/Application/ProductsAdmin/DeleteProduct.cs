using Domain.Infrastructure;
using System;
using System.Threading.Tasks;

namespace Application.ProductsAdmin
{
    [Service]
    public class DeleteProduct
    {
        private readonly IProductManager _productManager;

        public DeleteProduct(IProductManager productManager)
        {
            _productManager = productManager;
        }

        public async Task<bool> Do(int id)
        {
            if(await _productManager.DeleteProduct(id) != 1)
            {
                //create custom execption
                throw new Exception("Failed to Delete product");
            }
            
            return true;
        }
    }
  
}
