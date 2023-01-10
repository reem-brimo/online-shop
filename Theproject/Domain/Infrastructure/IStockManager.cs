using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Infrastructure
{
        public interface IStockManager
        {

            Task<int> CreateStock(Stock stock);
            Task<int> DeleteStockById(int id);
            Task<int> UpdateStock(List<Stock> stocks);
            Stock GetStock(int stockId);
            bool EnoughStock(int stockId, int num);
            Task PutStockOnHold(int stockId, int num, string sessionId);
            
            Task RetriveExpiredStockOnHold();

            Task RemoveStockFromHold(string sessionId);
            Task RemoveStockFromHold(int stockId, int num, string sessionId);
            
        }
    
}
