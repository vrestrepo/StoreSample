using Application.Common;
using Application.Interfaces;
using Domain.Interfaces;
using Domain.Models;

namespace Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<ServiceResponse<IEnumerable<PredictedOrderModel>>> GetPredictedOrdersAsync()
        {
            var employees = await _orderRepository.GetPredictedOrdersAsync();
            return new ServiceResponse<IEnumerable<PredictedOrderModel>>(true, "Lista de clientes con fecha de ultima orden y fecha de posible orden", employees);
        }

        public async Task<ServiceResponse<IEnumerable<ClientOrderModel>>> GetOrdersByClientAsync(int customerId)
        {
            var exists = await _orderRepository.ExistsCustomerAsync(customerId);
            if (!exists)
            {
                return new ServiceResponse<IEnumerable<ClientOrderModel>>(false, "El cliente no existe");
            }
            var orders = await _orderRepository.GetOrdersByClientAsync(customerId);
            return new ServiceResponse<IEnumerable<ClientOrderModel>>(true, "Lista de ordenes por cliente", orders);
        }

        public async Task<ServiceResponse<int>> AddOrderAsync(NewOrderModel newOrder)
        {
            try
            {
                int result = await _orderRepository.AddOrderAsync(newOrder);
                return new ServiceResponse<int>(true, "Orden creada exitosamente", result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new ServiceResponse<int>(false, "Error al crear la orden", 0);
            }
        }
    }
}
