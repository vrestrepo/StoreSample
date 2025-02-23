using Application.Common;
using Application.Interfaces;
using Domain.Interfaces;
using Domain.Models;

namespace Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ServiceResponse<IEnumerable<ProductModel>>> GetProductsAsync()
        {
            var products = await _productRepository.GetProductsAsync();
            return new ServiceResponse<IEnumerable<ProductModel>>(true, "Lista de productos", products);
        }
    }
}
