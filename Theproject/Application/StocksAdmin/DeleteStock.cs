using DataBase;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.StocksAdmin
{
    class DeleteStock
    {
        private readonly ApplicationDbContext _context;

        public DeleteStock(ApplicationDbContext context)
        {
            _context = context;

        }

        public async Task<bool> Do(int id)
        {
            var stock = _context.Stocks.FirstOrDefault(x => x.Id == id);
            _context.Remove(stock);
            await _context.SaveChangesAsync();

            return true;
        }

    }
}

