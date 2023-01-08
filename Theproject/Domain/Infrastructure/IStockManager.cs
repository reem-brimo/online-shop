using Domain.Models;
using System.Threading.Tasks;

namespace Domain.Infrastructure
{
        public interface IStockManager
        {
            Stock GetStock(int stockId);
            bool EnoughStock(int stockId, int num);
            Task PutStockOnHold(int stockId, int num, string sessionId);
            Task RemoveStockFromHold(int stockId, int num, string sessionId);
        }
    
}
