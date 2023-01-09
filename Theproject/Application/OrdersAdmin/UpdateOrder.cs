using Domain.Infrastructure;
using System.Threading.Tasks;

namespace Application.OrdersAdmin
{
    public class UpdateOrder
    {
        private readonly IOrderManager _orderManager;

        public UpdateOrder(IOrderManager orderManager)
        {
            _orderManager = orderManager;
        }

        public Task<int> DoAysnc(int id)
        {
            return _orderManager.UpdateOrder(id);
        }

    }
}
