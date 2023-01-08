using Domain.Infrastructure;
using Domain.Models;
using System.Threading.Tasks;

namespace Application.Cart
{
    public class AddToCart
    {
        private ISessionManager _sessionManager;
        private  IStockManager _stockManager;

        public AddToCart(ISessionManager sessionManager, IStockManager stockManager)
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

            //TODO: adding filters for handling update expration date

            //service responsibility
            if (!_stockManager.EnoughStock(request.StockId, request.Num))
            {
                return false;
            }

            await _stockManager.PutStockOnHold(request.StockId, request.Num, _sessionManager.GetId());

            var stock = _stockManager.GetStock(request.StockId);

            var cartProduct = new CartProduct
            {
                ProductId = stock.ProductId,
                ProductName = stock.Product.Name,
                StockId = stock.Id,
                Num = request.Num,
                Price = stock.Product.Price
                

            };
            _sessionManager.AddProduct(cartProduct);

            return true;
        }
    }
}
