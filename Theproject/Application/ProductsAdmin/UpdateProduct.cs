using Domain.Infrastructure;
using System;
using System.Threading.Tasks;

namespace Application.ProductsAdmin
{
    [Service]
    public class UpdateProduct
    {
        private readonly IProductManager _productManager;

        public UpdateProduct(IProductManager productManager)
        {
            _productManager = productManager;
        }


        public async Task<Response> Do(ProductViewModel product)
        {


            var oldProduct = _productManager.GetProductById(product.Id, x => x);


            oldProduct.Name = product.Name;
            oldProduct.Description = product.Description;

            var priceString = product.Price.Replace("$", "").Trim();

            oldProduct.Price = Convert.ToDouble(priceString);


            await _productManager.UpdateProduct(oldProduct);

            return new Response
            {
                Id = oldProduct.Id,
                Name = oldProduct.Name,
                Description = oldProduct.Description,
                Price = oldProduct.Price.GetPriceString(),

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
