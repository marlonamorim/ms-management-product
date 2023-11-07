using MGM.MS.Management.Product.Infrastructure.Entities;

namespace MGM.MS.Management.Product.Infrastructure.Interfaces
{
    public interface IProductRepository
    {
        Task UpsertAsync(ProductEntity entity);

        Task<IEnumerable<ProductEntity>> ListByCategoryAsync(string categoryId);

        Task<string?> GetDetailsAsync(string id);

        Task DeleteAsync(string id);
    }
}
