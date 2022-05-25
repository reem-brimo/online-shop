using Application.ProductsAdmin;
using Application.StocksAdmin;
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

        [HttpGet("products/all")]
        public IActionResult GetProducts() => Ok(new GetProducts(_context).Do());
        
        [HttpGet("products/{id}")]
        public IActionResult GetProduct(int id) => Ok(new GetProduct(_context).Do(id));
        
        [HttpPost("products")]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProduct.ProductViewModel productView) => Ok(await new CreateProduct(_context).Do(productView));
        
        [HttpDelete("products/{id}")]
        public async Task<IActionResult>  DeleteProduct(int id) => Ok(await new DeleteProduct(_context).Do(id));
        
        [HttpPut("products")]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProduct.ProductViewModel productView) => Ok(await new UpdateProduct(_context).Do(productView));[HttpGet("products")]


        [HttpGet("stocks")]
        public IActionResult GetStocks() => Ok(new GetStock(_context).Do());
        
        [HttpPost("stocks")]
        public async Task<IActionResult> CreateStock([FromBody] CreateStock.Request StockView) => Ok(await new CreateStock(_context).Do(StockView));
        
        [HttpDelete("stocks/{id}")]
        public async Task<IActionResult>  DeleteStock(int id) => Ok(await new DeleteStock(_context).Do(id));
        
        [HttpPut("stocks")]
        public async Task<IActionResult> UpdateStock([FromBody] UpdateStock.Request StockView) => Ok(await new UpdateStock(_context).Do(StockView));
    }
}
