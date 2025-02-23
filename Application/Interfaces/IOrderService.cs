using Application.Common;
using Domain.Models;

namespace Application.Interfaces
{
    public interface IOrderService
    {
        Task<ServiceResponse<IEnumerable<PredictedOrderModel>>> GetPredictedOrdersAsync();
        Task<ServiceResponse<IEnumerable<ClientOrderModel>>> GetOrdersByClientAsync(int customerId);
        Task<ServiceResponse<int>> AddOrderAsync(NewOrderModel newOrder);
    }
}