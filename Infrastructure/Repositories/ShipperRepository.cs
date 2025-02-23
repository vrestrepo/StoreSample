using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using AppDbContext = Infrastructure.Persistence.AppDbContext;

namespace Infrastructure.Repositories
{
    public class ShipperRepository : IShipperRepository
    {
        private readonly AppDbContext _context;

        public ShipperRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ShipperModel>> GetShippersAsync()
        {
            return await _context.Shippers
                .Select(s => new ShipperModel
                {
                    ShipperId = s.Shipperid,
                    CompanyName = s.Companyname
                })
                .ToListAsync();
        }
    }
}