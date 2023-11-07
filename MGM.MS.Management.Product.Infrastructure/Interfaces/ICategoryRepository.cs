using MGM.MS.Management.Product.Infrastructure.Entities;

namespace MGM.MS.Management.Product.Infrastructure.Interfaces
{
    public interface ICategoryRepository
    {
        Task UpsertAsync(CategoryEntity entity);

        Task<IEnumerable<CategoryEntity>> ListAsync();

        Task DeleteAsync(string id);
    }
}
