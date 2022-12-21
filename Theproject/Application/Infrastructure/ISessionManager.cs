using Domain.Models;
using System.Collections.Generic;

namespace Application.Infrastructure
{
    public interface ISessionManager
    {
        string GetId();
        void AddProduct(int stockId, int num);
        void RemoveProduct(int stockId, int num, bool all);
        List<CartProduct> GetCart();

        void AddCustomerInfo(CustomerInformation customer);
        CustomerInformation GetCustomerInformation();

    }
}
