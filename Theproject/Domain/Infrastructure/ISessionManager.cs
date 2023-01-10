using Domain.Models;
using System;
using System.Collections.Generic;

namespace Domain.Infrastructure
{
    public interface ISessionManager
    {
        string GetId();
        void AddProduct(CartProduct cartProduct);
        void RemoveProduct(int stockId, int num);
        IEnumerable<TResult> GetCart<TResult>(Func<CartProduct, TResult> selector);
        void ClearCart();
        void AddCustomerInfo(CustomerInformation customer);
        CustomerInformation GetCustomerInformation();

    }
}
