using Application.StocksAdmin;
using DataBase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ShopUI.Controllers
{   [Route("[controller]")]
    [Authorize(Policy = "Manager")]

    public class StocksController: Controller
    {
        private readonly ApplicationDbContext _context;
    public StocksController(ApplicationDbContext ctx)
    {
        _context = ctx;
    }


    [HttpGet]
    public IActionResult GetStocks() => Ok(new GetStock(_context).Do());

    [HttpPost]
    public async Task<IActionResult> CreateStock([FromBody] CreateStock.Request StockView) => Ok(await new CreateStock(_context).Do(StockView));

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStock(int id) => Ok(await new DeleteStock(_context).Do(id));

    [HttpPut]
    public async Task<IActionResult> UpdateStock([FromBody] UpdateStock.Request StockView) => Ok(await new UpdateStock(_context).Do(StockView));
    
    }
}
