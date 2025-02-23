using Application.Common;
using Application.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace StoreSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("predicted-orders")]
        [ProducesResponseType(typeof(ServiceResponse<IEnumerable<PredictedOrderModel>>), 200)]
        [ProducesResponseType(typeof(ServiceResponse<IEnumerable<PredictedOrderModel>>), 400)]
        public async Task<ActionResult<ServiceResponse<IEnumerable<PredictedOrderModel>>>> GetPredictedOrders()
        {
            var response = await _orderService.GetPredictedOrdersAsync();
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("client-orders/{customerId}")]
        [ProducesResponseType(typeof(ServiceResponse<IEnumerable<ClientOrderModel>>), 200)]
        [ProducesResponseType(typeof(ServiceResponse<IEnumerable<ClientOrderModel>>), 400)]
        public async Task<ActionResult<ServiceResponse<IEnumerable<ClientOrderModel>>>> GetOrdersByClient(int customerId)
        {
            var response = await _orderService.GetOrdersByClientAsync(customerId);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("create-order")]
        public async Task<ActionResult> AddOrder([FromBody] NewOrderModel newOrder)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                return BadRequest(new ServiceResponse<List<string>>(false, "Errores de validación", errors));
            }
            var response = await _orderService.AddOrderAsync(newOrder);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return CreatedAtAction(nameof(AddOrder), new { orderId = response.Data }, response);
        }
    }
}
