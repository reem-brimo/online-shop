﻿using DataBase;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Orders
{
    public class CreateOrder
    {
        private ApplicationDbContext _context;
        public CreateOrder(ApplicationDbContext context)
        {
            _context = context;
        }

        public class Request {
            public string StripeReference { get; set; }
            public string  SessionId { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }

            public string Address1 { get; set; }
            public string Address2 { get; set; }
            public string PostCode { get; set; }
            public string City { get; set; }
            public List<Stock> Stocks { get; set; }
        }

        public class Stock
        {
            public int StockId { get; set; }
            public int Num { get; set; }

        }
        public async Task<bool> Do(Request request)
        {

            var stocksOnHold = _context.StocksOnHold
                                 .Where(x => x.SessionId == request.SessionId)
                                 .ToList();

            _context.StocksOnHold.RemoveRange(stocksOnHold);

            var order = new Order
            {
                OrderRef = CreateOrderReference(),
                StripeReference = request.StripeReference,

                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Address1 = request.Address1,
                Address2 = request.Address2,
                City = request.City,
                PostCode = request.PostCode,

                OrderStocks = request.Stocks.Select(x => new OrderStock
                {
                    StockId = x.StockId,
                    Num = x.Num
                }).ToList()

            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return true;
        }

        public string CreateOrderReference()
        {
            //25 char
            Guid guid = Guid.NewGuid();
            var orderReference = Convert.ToBase64String(guid.ToByteArray());
            return new String(orderReference);
        }
    }
}
