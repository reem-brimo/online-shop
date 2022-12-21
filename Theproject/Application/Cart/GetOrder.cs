using Application.Infrastructure;
using DataBase;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;


namespace Application.Cart
{
    public class GetOrder
    {
        private ApplicationDbContext _context;
        private ISessionManager _sessionManager;
        public GetOrder(ISessionManager sessionManager, ApplicationDbContext context)
        {
            _sessionManager = sessionManager;
            _context = context;
        }

        public class Product
        {
            public int ProductId { get; set; }
            public int Value { get; set; }

            public int StockId { get; set; }
            public int Num { get; set; }
        }

        public class Response
        {
            public IEnumerable<Product> Products { get; set; }
            public CustomerInformation CustomerInformation { get; set; }

            public int GetTotalCharge() => Products.Sum(x => x.Value * x.Num);
        }
        public class CustomerInformation
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }

            public string Address1 { get; set; }
            public string Address2 { get; set; }
            public string PostCode { get; set; }
            public string City { get; set; }
        }

        public Response Do()
        {
            //todo: account for multipule  items in the cart

            var cart = _sessionManager.GetCart();

            if (cart == null)
                return null;

            var productsList = _context.Stocks
                .Include(x => x.Product)
                .AsEnumerable()
                .Where(x => cart.Any(y => y.StockId == x.Id))
                .Select(x => new Product
                {
                    ProductId = x.ProductId,
                    StockId = x.Id,
                    Value = (int)(x.Product.Price * 100),
                    Num = cart.FirstOrDefault(y => y.StockId == x.Id).Num

                })
                .ToList();

            var customerInformation = _sessionManager.GetCustomerInformation();

            if (customerInformation == null)
                return null;

            //todo appendding the cart
            return new Response
            {
                Products = productsList,
                CustomerInformation = new CustomerInformation
                {
                    FirstName = customerInformation.FirstName,
                    LastName = customerInformation.LastName,
                    Email = customerInformation.FirstName,
                    PhoneNumber = customerInformation.PhoneNumber,
                    PostCode = customerInformation.PostCode,
                    City = customerInformation.City,
                    Address1 = customerInformation.FirstName,
                    Address2 = customerInformation.FirstName,
                }
            };
        }
    }
}
