using MGM.MS.Management.Product.Infrastructure.Entities;
using MGM.MS.Management.Product.Infrastructure.Interfaces;
using MGM.MS.Management.Product.Notification.Interfaces;
using MGM.MS.Management.Product.Services.Dtos;
using MGM.MS.Management.Product.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace MGM.MS.Management.Product.Services.Services
{
    internal class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly INotification _notification;
        private readonly ILogger<CategoryService> _logger;

        public CategoryService(ICategoryRepository categoryRepository,
            INotification notification,
            ILogger<CategoryService> logger)
        {
            _categoryRepository = categoryRepository;
            _notification = notification;
            _logger = logger;
        }

        public async Task DeleteAsync(string id)
            => await ProcessAsync(async () => await _categoryRepository.DeleteAsync(id));

        public async Task<IEnumerable<CategoryDto>?> ListAllAsync()
        {
            var ret = await ProcessAsync<IEnumerable<CategoryEntity>?>(async () => await _categoryRepository.ListAsync());
            return ret is not null && ret.Any() ?
                ret.Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                }) :
                default;
        }

        public async Task UpsertAsync(CategoryDto dto)
        {
            var entity = new CategoryEntity(dto.Id, dto.Name, dto.Description);
            await ProcessAsync(async () => await _categoryRepository.UpsertAsync(entity));
        }

        private async Task ProcessAsync(Func<Task> func)
        {
            try
            {
                await func().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                _notification.AddNotification(500, "Ocorreu um erro interno. Consulte o admin do sistema.");
            }
        }

        private async Task<T?> ProcessAsync<T>(Func<Task<T>> func)
        {
            try
            {
                T result = await func().ConfigureAwait(false);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                _notification.AddNotification(500, "Ocorreu um erro interno. Consulte o admin do sistema.");
                
                return default;
            }
        }
    }
}
