using Application.Infrastructure;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace ShopUI.Infrastructure
{
    public class SessionManager : ISessionManager
    {

        private readonly ISession _session;

        public SessionManager(IHttpContextAccessor httpContextAccessor)
        {
            _session = httpContextAccessor.HttpContext.Session;
        }

        public void AddCustomerInfo(CustomerInformation customer)
        {
            var stringObject = JsonConvert.SerializeObject(customer);

            _session.SetString("customer.info", stringObject);
        }

        public void AddProduct(int stockId, int num)
        {

            var cartList = new List<CartProduct>();
            var stringObject = _session.GetString("cart");

            if (!string.IsNullOrEmpty(stringObject))
            {
                cartList = JsonConvert.DeserializeObject<List<CartProduct>>(stringObject);
            }

            if (cartList.Any(x => x.StockId == stockId))
            {
                cartList.Find(x => x.StockId == stockId).Num += num;
            }
            else
            {
                cartList.Add(new CartProduct
                {
                    StockId = stockId,
                    Num = num,
                });

            }

            stringObject = JsonConvert.SerializeObject(cartList);
            _session.SetString("cart", stringObject);
        }

        public List<CartProduct> GetCart()
        {
            var stringObject = _session.GetString("cart");

            if (string.IsNullOrEmpty(stringObject))
                return null;

            var CartList = JsonConvert.DeserializeObject<List<CartProduct>>(stringObject);

            return CartList;
        }

        public CustomerInformation GetCustomerInformation()
        {
            var stringObject = _session.GetString("customer.info");

            if (string.IsNullOrEmpty(stringObject))
                return null;

            var customerInformation = JsonConvert.DeserializeObject<CustomerInformation>(stringObject);

            return customerInformation;
        }

        public string GetId() => _session.Id;

        public void RemoveProduct(int stockId, int num, bool all)
        {

            var cartList = new List<CartProduct>();
            var stringObject = _session.GetString("cart");

            if (string.IsNullOrEmpty(stringObject))  return;
            
            cartList = JsonConvert.DeserializeObject<List<CartProduct>>(stringObject);

            if (!cartList.Any(x => x.StockId == stockId)) return;

            var product = cartList.Find(x => x.StockId == stockId);

            if (!all)
                product.Num -= num;

            else
                product.Num = 0;

            if (product.Num <= 0)
                cartList.Remove(product);

            stringObject = JsonConvert.SerializeObject(cartList);

            _session.SetString("cart", stringObject);
        }
    }
}
