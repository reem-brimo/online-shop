using Application.Products.ViewModels;
using Application.ProductsAdmin;
using DataBase;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopUI.Controllers
{
    [Route("[controller]")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        public AdminController(ApplicationDbContext ctx)
        {
            _context = ctx;
        } 

        [HttpGet("products")]
        public IActionResult GetProducts() => Ok(new GetProducts(_context).Do());
        
        [HttpGet("products/{id}")]
        public IActionResult GetProduct(int id) => Ok(new GetProduct(_context).Do(id));
        
        [HttpPost("products")]
        public async Task<IActionResult> CreateProduct([FromBody]ProductViewModel productView) => Ok(await new CreateProduct(_context).Do(productView));
        
        [HttpDelete("products/{id}")]
        public async Task<IActionResult>  DeleteProduct(int id) => Ok(await new DeleteProduct(_context).Do(id));
        
        [HttpPut("products")]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductViewModel productView) => Ok(await new UpdateProduct(_context).Do(productView));
    }
}
