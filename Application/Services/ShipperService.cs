using Application.Common;
using Application.Interfaces;
using Domain.Interfaces;
using Domain.Models;

namespace Application.Services
{
    public class ShipperService : IShipperService
    {
        private readonly IShipperRepository _shipperRepository;

        public ShipperService(IShipperRepository shipperRepository)
        {
            _shipperRepository = shipperRepository;
        }

        public async Task<ServiceResponse<IEnumerable<ShipperModel>>> GetShippersAsync()
        {
            var shippers = await _shipperRepository.GetShippersAsync();
            return new ServiceResponse<IEnumerable<ShipperModel>>(true, "Lista de transportistas", shippers);
        }
    }
}