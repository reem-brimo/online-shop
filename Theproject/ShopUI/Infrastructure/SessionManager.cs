using Domain.Infrastructure;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShopUI.Infrastructure
{
    public class SessionManager : ISessionManager
    {
        private const string CartKey = "cart";
        private const string CustumerInfoKey = "customer.info";

        private readonly ISession _session;

        public SessionManager(IHttpContextAccessor httpContextAccessor)
        {
            _session = httpContextAccessor.HttpContext.Session;
        }

        public void AddCustomerInfo(CustomerInformation customer)
        {
            var stringObject = JsonConvert.SerializeObject(customer);

            _session.SetString(CustumerInfoKey, stringObject);
        }

        public void AddProduct(CartProduct cartProduct)
        {

            var cartList = new List<CartProduct>();
            var stringObject = _session.GetString(CartKey);

            if (!string.IsNullOrEmpty(stringObject))
            {
                cartList = JsonConvert.DeserializeObject<List<CartProduct>>(stringObject);
            }

            if (cartList.Any(x => x.StockId == cartProduct.StockId))
            {
                cartList.Find(x => x.StockId == cartProduct.StockId).Num += cartProduct.Num;
            }
            else
            {
                cartList.Add(cartProduct);

            }

            stringObject = JsonConvert.SerializeObject(cartList);
            _session.SetString(CartKey, stringObject);
        }

        public void ClearCart()
        {
            _session.Remove(CartKey);
        }

        public IEnumerable<TResult> GetCart<TResult>(Func<CartProduct, TResult> selector)
        {
            var stringObject = _session.GetString(CartKey);

            if (string.IsNullOrEmpty(stringObject))
                return new List<TResult>();

            var CartList = JsonConvert.DeserializeObject<IEnumerable<CartProduct>>(stringObject);

            return CartList.Select(selector);
        }

        public CustomerInformation GetCustomerInformation()
        {
            var stringObject = _session.GetString(CustumerInfoKey);

            if (string.IsNullOrEmpty(stringObject))
                return null;

            var customerInformation = JsonConvert.DeserializeObject<CustomerInformation>(stringObject);

            return customerInformation;
        }

        public string GetId() => _session.Id;

        public void RemoveProduct(int stockId, int num)
        {

            var cartList = new List<CartProduct>();
            var stringObject = _session.GetString(CartKey);

            if (string.IsNullOrEmpty(stringObject))  return;
            
            cartList = JsonConvert.DeserializeObject<List<CartProduct>>(stringObject);

            if (!cartList.Any(x => x.StockId == stockId)) return;

            var product = cartList.Find(x => x.StockId == stockId);

                product.Num -= num;


            if (product.Num <= 0)
                cartList.Remove(product);

            stringObject = JsonConvert.SerializeObject(cartList);

            _session.SetString(CartKey, stringObject);
        }
    }
}
