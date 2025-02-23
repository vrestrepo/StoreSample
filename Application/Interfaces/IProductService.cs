using Application.Common;
using Domain.Models;

namespace Application.Interfaces
{
    public interface IProductService
    {
        Task<ServiceResponse<IEnumerable<ProductModel>>> GetProductsAsync();
    }
}
