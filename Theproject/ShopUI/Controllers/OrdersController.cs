using Application.OrdersAdmin;
using DataBase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ShopUI.Controllers
{
    [Route("[controller]")]
    [Authorize(Policy = "Manager")]

    public class OrdersController : Controller
    {

        [HttpGet]
        public IActionResult GetOrders(
            int status,
            [FromServices] GetOrders getOrders) =>
                Ok(getOrders.Do(status));

        [HttpGet("{id}")]
        public IActionResult GetOrder(
            int id,
            [FromServices] GetOrder getOrder) =>
                Ok(getOrder.Do(id));

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(
            int id,
            [FromServices] UpdateOrder updateOrder) => 
                Ok(await updateOrder.DoAysnc(id));

    }
}
