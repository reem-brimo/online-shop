using Domain.Infrastructure;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Orders
{
    [Service]
    public class GetOrder
    {

        private readonly IOrderManager _orderManager;

        public GetOrder(IOrderManager orderManager)
        {
            _orderManager = orderManager;
        }
        public class Response
        {
            public string OrderRef { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }

            public string Address1 { get; set; }
            public string Address2 { get; set; }
            public string PostCode { get; set; }
            public string City { get; set; }

            public IEnumerable<Product> Products { get; set; }
            public string TotalValue { get; set; }
        }

        public class Product
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public string Price { get; set; }
            public int Num { get; set; }
            public string StockDescription { get; set; } //size or type
        }

        public Response Do(string reference) =>
            _orderManager.GetOrderByReference(reference, Projection);


        private static Func<Order, Response> Projection = (order) =>
            new Response
            {
                OrderRef = order.OrderRef,
                FirstName = order.FirstName,
                LastName = order.LastName,
                Email = order.Email,
                PhoneNumber = order.PhoneNumber,
                Address1 = order.Address1,
                Address2 = order.Address2,
                City = order.City,
                PostCode = order.PostCode,

                Products = order.OrderStocks
                                .Select(y => new Product
                                {
                                    Name = y.Stock.Product.Name,
                                    Description = y.Stock.Product.Description,
                                    Price = $"$ {y.Stock.Product.Price.ToString("N2")}",
                                    Num = y.Stock.Num, // quantity in the stock
                                    StockDescription = y.Stock.Descripion,

                                }),

                TotalValue = order.OrderStocks.Sum(y => y.Stock.Product.Price)
                                                                 .ToString("N2")
            };






    }
}
