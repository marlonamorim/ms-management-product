using MGM.MS.Management.Product.Infrastructure.Entities;
using MGM.MS.Management.Product.Services.Dtos;

namespace MGM.MS.Management.Product.Services.Interfaces
{
    public interface IProductService
    {
        Task UpsertAsync(ProductDto entity);

        Task<IEnumerable<ProductDto>?> ListByCategoryAsync(string categoryId);

        Task<string?> GetDetailsAsync(string id);

        Task DeleteAsync(string id);
    }
}
