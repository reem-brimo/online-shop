using Domain.Enums;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Infrastructure
{
    public interface IOrderManager
    {
        TResult GetOrderByReference<TResult>(string reference, Func<Order, TResult> selector);
        TResult GetOrderById<TResult>(int id, Func<Order, TResult> selector);
        IEnumerable<TResult> GetOrdersByStatus<TResult>(OrderStatus status,Func<Order,TResult> selector);

        Task<int> UpdateOrder(int id);
        Task<int> CreateOrder(Order order);

    }
}
