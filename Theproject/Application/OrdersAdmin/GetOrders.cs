using Domain.Enums;
using Domain.Infrastructure;
using System.Collections.Generic;

namespace Application.OrdersAdmin
{
    [Service]
    public class GetOrders
    {
        private readonly IOrderManager _orderManager;

        public GetOrders(IOrderManager orderManager)
        {
            _orderManager = orderManager;
        }
        public class Response
        {
            public int Id { get; set; }
            public string OrderRef { get; set; }        
            public string Email { get; set; }
        }

        public IEnumerable<Response> Do(int status)
        {
            return _orderManager.GetOrdersByStatus((OrderStatus)status, x => new Response
            {
                Id = x.Id,
                OrderRef = x.OrderRef,
                Email = x.Email
            });
        }

    }
}
