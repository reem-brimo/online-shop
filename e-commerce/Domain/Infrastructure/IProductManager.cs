﻿using Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Infrastructure
{
    public interface IProductManager 
    {
        Task<int> CreateProduct(Product product);
        Task<int> DeleteProduct(int id);
        Task<int> UpdateProduct(Product product);
        TResult GetProductById<TResult>(int id, Func<Product, TResult> selector);

        TResult GetProductByName<TResult>(string name, Func<Product, TResult> selector);
        IEnumerable<TResult> GetProducts<TResult>(Func<Product, TResult> selector);
    }
}
