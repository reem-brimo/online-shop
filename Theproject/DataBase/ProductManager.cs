﻿using Domain.Infrastructure;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataBase
{
    public class ProductManager : IProductManager
    {
        private ApplicationDbContext _context;

        public ProductManager(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<int> CreateProduct(Product product)
        {
            _context.Products.Add(product);
            return  _context.SaveChangesAsync();
        }

        public Task<int> DeleteProduct(int id)
        {
            var product = _context.Products.FirstOrDefault(x => x.Id == id);
            _context.Products.Remove(product);

            return _context.SaveChangesAsync();      
        }

        public TResult GetProductById<TResult>(int id, Func<Product, TResult> selector)
        {
           return _context.Products
                   .Where(x => x.Id == id)
                   .Select(selector)
                   .FirstOrDefault();
        }

        public TResult GetProductByName<TResult>(string name, Func<Product, TResult> selector)
        {
            return _context.Products
                            .Include(x => x.Stock)
                            .Where(x => x.Name == name)
                            .Select(selector)
                            .FirstOrDefault();
        }

        public IEnumerable<TResult> GetProducts<TResult>(Func<Product, TResult> selector)
        {
           return _context.Products
                    .Include(x => x.Stock)
                    .Select(selector)
                    .ToList(); 

        }

        public Task<int> UpdateProduct(Product product)
        {
            _context.Products.Update(product);
           return _context.SaveChangesAsync();

        }
    }
}
