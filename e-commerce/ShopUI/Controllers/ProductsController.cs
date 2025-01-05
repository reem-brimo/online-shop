using Application.ProductsAdmin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ShopUI.Controllers
{
    [Route("[controller]")]
    [Authorize(Policy = "Manager")]

    public class ProductsController : Controller
    {

        [HttpGet]
        public IActionResult GetProducts([FromServices] GetProducts getProducts) => Ok(getProducts.Do());


        [HttpGet("{id}")]
        public IActionResult GetProduct([FromServices] GetProduct getProduct,
            int id) => Ok(getProduct.Do(id));

        [HttpPost]
        public async Task<IActionResult> CreateProduct(
            [FromBody] CreateProduct.ProductViewModel productView,
            [FromServices] CreateProduct createProduct
            ) => Ok(await createProduct.Do(productView));

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id,
            [FromServices] DeleteProduct deleteProduct) => Ok(await deleteProduct.Do(id));

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(
            [FromBody] UpdateProduct.ProductViewModel productView,
            [FromServices] UpdateProduct updateProduct) => Ok(await updateProduct.Do(productView));

    }


}
