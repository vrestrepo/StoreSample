using Domain.Models;

namespace Domain.Interfaces
{
    public interface IOrderRepository
    {
        Task<IEnumerable<PredictedOrderModel>> GetPredictedOrdersAsync();
        Task<IEnumerable<ClientOrderModel>> GetOrdersByClientAsync(int customerId);
        Task<int> AddOrderAsync(NewOrderModel newOrder);
        Task<bool> ExistsCustomerAsync(int customerId);
    }
}