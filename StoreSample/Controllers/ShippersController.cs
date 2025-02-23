using Application.Common;
using Application.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace StoreSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShippersController : ControllerBase
    {
        private readonly IShipperService _shipperService;

        public ShippersController(IShipperService shipperService)
        {
            _shipperService = shipperService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ServiceResponse<IEnumerable<ShipperModel>>), 200)]
        [ProducesResponseType(typeof(ServiceResponse<IEnumerable<ShipperModel>>), 400)]
        public async Task<ActionResult<ServiceResponse<IEnumerable<ShipperModel>>>> GetShippers()
        {
            var response = await _shipperService.GetShippersAsync();
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
