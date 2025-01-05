using Domain.Enums;
using Domain.Infrastructure;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataBase
{
    public class OrderManager : IOrderManager
    {
        private readonly ApplicationDbContext _context;

        public OrderManager(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }

        private TResult GetOrder<TResult>(
            Func<Order, bool> condition,
            Func<Order, TResult> selector
            )
        {
            return _context.Orders
                          .Where(x => condition(x))
                          .Include(x => x.OrderStocks)
                           .ThenInclude(x => x.Stock)
                            .ThenInclude(x => x.Product)
                          .Select(selector)
                          .FirstOrDefault();
        }

        public IEnumerable<TResult> GetOrdersByStatus<TResult>(OrderStatus status, Func<Order, TResult> selector)
        {
            return _context.Orders
                    .Where(x => x.Status == status)
                    .Select(selector)
                    .ToList();

        }

        public TResult GetOrderById<TResult>(int id, Func<Order, TResult> selector)
        {
            return GetOrder(order => order.Id == id, selector);
        }

        public TResult GetOrderByReference<TResult>(
            string reference,
            Func<Order, TResult> selector)
        {
            return GetOrder(order => order.OrderRef == reference, selector);
        }

        public Task<int> CreateOrder(Order order)
        {
            _context.Orders.Add(order);

            return _context.SaveChangesAsync();
        }

        public Task<int> UpdateOrder(int id)
        {
            _context.Orders.FirstOrDefault(x => x.Id == id).Status ++ ;
            return _context.SaveChangesAsync();
        }
    }
}
