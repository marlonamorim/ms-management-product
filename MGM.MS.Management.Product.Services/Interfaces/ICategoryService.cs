using MGM.MS.Management.Product.Services.Dtos;

namespace MGM.MS.Management.Product.Services.Interfaces
{
    public interface ICategoryService
    {
        Task UpsertAsync(CategoryDto dto);

        Task<IEnumerable<CategoryDto>?> ListAllAsync();

        Task DeleteAsync(string id);
    }
}
