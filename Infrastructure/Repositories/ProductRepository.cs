using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using AppDbContext = Infrastructure.Persistence.AppDbContext;

namespace Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductModel>> GetProductsAsync()
        {
            return await _context.Products
                .Select(p => new ProductModel
                {
                    ProductId = p.Productid,
                    ProductName = p.Productname
                })
                .ToListAsync();
        }
    }
}
