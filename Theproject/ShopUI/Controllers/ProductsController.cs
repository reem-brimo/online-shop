using Application.ProductsAdmin;
using DataBase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ShopUI.Controllers
{
    [Route("[controller]")]
    [Authorize(Policy = "Manager")]

    public class ProductsController : Controller
    {
        private ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetProducts() => Ok(new GetProducts(_context).Do());

        [HttpGet("{id}")]
        public IActionResult GetProduct(int id) => Ok(new GetProduct(_context).Do(id));

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProduct.ProductViewModel productView) => Ok(await new CreateProduct(_context).Do(productView));

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id) => Ok(await new DeleteProduct(_context).Do(id));

        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProduct.ProductViewModel productView) => Ok(await new UpdateProduct(_context).Do(productView));

    }


}
