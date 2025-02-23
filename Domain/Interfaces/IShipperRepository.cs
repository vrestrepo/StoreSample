using Domain.Models;

namespace Domain.Interfaces
{
    public interface IShipperRepository
    {
        Task<IEnumerable<ShipperModel>> GetShippersAsync();
    }
}