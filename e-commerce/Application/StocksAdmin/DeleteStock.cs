using Domain.Infrastructure;
using System.Threading.Tasks;

namespace Application.StocksAdmin
{
    [Service]
    public class DeleteStock
    {
        private readonly IStockManager _stockManager;

        public DeleteStock(IStockManager stockManager)
        {
            _stockManager = stockManager;
        }

        public async Task<bool> Do(int id)
        {
            await _stockManager.DeleteStockById(id);

            return true;
        }

    }
}

