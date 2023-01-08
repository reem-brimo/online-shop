using Domain.Infrastructure;
using DataBase;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Cart
{
    public class RemoveFromCart
    {
        private ISessionManager _sessionManager;
        private IStockManager _stockManager;

        public RemoveFromCart(ISessionManager sessionManager, IStockManager stockManager)
        {
            _sessionManager = sessionManager;
            _stockManager = stockManager;
        }
        public class Request
        {
            public int StockId { get; set; }
            public int Num { get; set; }

        }

     
        public async Task<bool> Do(Request request)
        {
            if(request.Num <= 0)
            {
                return false;
            }

            await _stockManager
                .RemoveStockFromHold(request.StockId, request.Num, _sessionManager.GetId());

            _sessionManager.RemoveProduct(request.StockId, request.Num);

       
            return true;
        }
    }
}
