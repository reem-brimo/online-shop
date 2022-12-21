using Application.Infrastructure;
using DataBase;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Cart
{
    public class RemoveFromCart
    {
        private ApplicationDbContext _context;
        private ISessionManager _sessionManager;
        public RemoveFromCart(ISessionManager sessionManager, ApplicationDbContext context)
        {
            _context = context;
            _sessionManager = sessionManager;
        }
        public class Request
        {
            public int StockId { get; set; }
            public int Num { get; set; }
            public bool All { get; set; }

        }
        public async Task<bool> Do(Request request)
        {

            _sessionManager.RemoveProduct(request.StockId, request.Num, request.All);

            var stockOnHold = _context.StocksOnHold
                .FirstOrDefault(x => x.StockId == request.StockId
                                && x.SessionId == _sessionManager.GetId());

            if (stockOnHold != null)
            {
                var stock = _context.Stocks.FirstOrDefault(x => x.Id == request.StockId);

                if (request.All)
                {
                    stock.Num += stockOnHold.Num;
                    stockOnHold.Num = 0;
                }
                else
                {
                    stock.Num += request.Num;
                    stockOnHold.Num -= request.Num;
                }


                if (stockOnHold.Num <= 0)
                {
                    _context.Remove(stockOnHold);
                }
                await _context.SaveChangesAsync();
            }

            else
            {
                //error
                return false;
            }

            return true;
        }
    }
}
