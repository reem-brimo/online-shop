using Application.StocksAdmin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ShopUI.Controllers
{   [Route("[controller]")]
    [Authorize(Policy = "Manager")]

    public class StocksController: Controller
    {

    [HttpGet]
    public IActionResult GetStocks([FromServices] GetStock getStock) => Ok(getStock.Do());

    [HttpPost]
    public async Task<IActionResult> CreateStock(
        [FromBody] CreateStock.Request StockView,
        [FromServices] CreateStock createStock) => Ok(await createStock.Do(StockView));

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStock(
        int id,
        [FromServices] DeleteStock deleteStock) => Ok(await deleteStock.Do(id));

    [HttpPut]
    public async Task<IActionResult> UpdateStock(
        [FromBody] UpdateStock.Request StockView,
        [FromServices] UpdateStock updateStock) => Ok(await updateStock.Do(StockView));
    
    }
}
