using DataBase;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.OrdersAdmin
{
    public class GetOrders
    {
        private ApplicationDbContext _context;

        public GetOrders(ApplicationDbContext context)
        {
            _context = context;
        }
        public class Response
        {
            public int Id { get; set; }
            public string OrderRef { get; set; }        
            public string Email { get; set; }
        }

        public IEnumerable<Response> Do(int status) =>
            _context.Orders
                    .Where(x => x.Status == (OrderStatus)status)
                    .Select(x => new Response
                    {
                        Id = x.Id,
                        OrderRef = x.OrderRef,
                        Email = x.Email
                    })
                    .ToList();

    }
}
