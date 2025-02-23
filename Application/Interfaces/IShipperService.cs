using Application.Common;
using Domain.Models;

namespace Application.Interfaces
{
    public interface IShipperService
    {
        Task<ServiceResponse<IEnumerable<ShipperModel>>> GetShippersAsync();
    }
}