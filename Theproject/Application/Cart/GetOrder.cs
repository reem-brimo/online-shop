using DataBase;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Application.Cart
{
    public class GetOrder
    {
        private ApplicationDbContext _context;
        private ISession _session;
        public GetOrder(ISession session, ApplicationDbContext context)
        {
            _session = session;
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
            public CustomerInformation customerInformation { get; set; }

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
            var cart = _session.GetString("cart");

            var CartList = JsonConvert.DeserializeObject<List<CartProduct>>(cart);

            var productsList = _context.Stocks
                .Include(x => x.Product)
                .AsEnumerable()
                .Where(x => CartList.Any(y => y.StockId == x.Id))
                .Select(x => new Product
                {
                    ProductId = x.ProductId,
                    StockId = x.Id,
                    Value = (int) (x.Product.Price * 100),
                    Num = CartList.FirstOrDefault(y => y.StockId == x.Id).Num

                })
                .ToList();

            var customerInfoString = _session.GetString("customer.info");
            var customerInformation = JsonConvert.DeserializeObject<Domain.Models.CustomerInformation>(customerInfoString);


            //todo appendding the cart
            return new Response
            {
                Products = productsList,
                customerInformation = new CustomerInformation
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
