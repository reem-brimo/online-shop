using Application.OrdersAdmin;
using DataBase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopUI.Controllers
{
    [Route("[controller]")]
    [Authorize(Policy = "Manager")]

    public class OrdersController : Controller
    {
        private ApplicationDbContext _context;
        public OrdersController(ApplicationDbContext ctx)
        {
            _context = ctx;
        }

        [HttpGet]
        public IActionResult GetOrders(int status) => Ok(new GetOrders(_context).Do(status));

        [HttpGet("{id}")]
        public IActionResult GetOrder(int id) => Ok(new GetOrder(_context).Do(id));

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id) => Ok(await new UpdateOrder(_context).Do(id));

    }
}
