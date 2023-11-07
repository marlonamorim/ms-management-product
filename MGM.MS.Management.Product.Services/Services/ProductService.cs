using MGM.MS.Management.Product.Infrastructure.Entities;
using MGM.MS.Management.Product.Infrastructure.Interfaces;
using MGM.MS.Management.Product.Notification.Interfaces;
using MGM.MS.Management.Product.Services.Dtos;
using MGM.MS.Management.Product.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace MGM.MS.Management.Product.Services.Services
{
    internal class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly INotification _notification;
        private readonly ILogger<ProductService> _logger;

        public ProductService(IProductRepository productRepository,
            INotification notification,
            ILogger<ProductService> logger)
        {
            _productRepository = productRepository;
            _notification = notification;
            _logger = logger;
        }

        public async Task DeleteAsync(string id)
            => await ProcessAsync(async () => await _productRepository.DeleteAsync(id));

        public async Task<string?> GetDetailsAsync(string id)
        {
            var ret = await ProcessAsync<string?>(async () => await _productRepository.GetDetailsAsync(id));

            if (string.IsNullOrEmpty(ret))
            {
                _notification.AddNotification(404, $"Não foi encontrado nenhum produto para o id informado: {id}");
                return default;
            }

            return ret;
        }

        public async Task<IEnumerable<ProductDto>?> ListByCategoryAsync(string categoryId)
        {
            var ret = await ProcessAsync<IEnumerable<ProductEntity>?>(async () => await _productRepository.ListByCategoryAsync(categoryId));

            if (!ret?.Any() ?? true)
            {
                _notification.AddNotification(404, $"Não foi encontrado nenhuma categoria para o id informado: {categoryId}");
                return default;
            }

            return ret.Select(x => new ProductDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    CategoryId = categoryId,
                    Details = x.Details,
                    Price = x.Price
                });
        }

        public async Task UpsertAsync(ProductDto dto)
        {
            var entity = new ProductEntity(dto.Id, dto.Name, dto.Description, dto.Price, dto.Details, dto.CategoryId);
            await ProcessAsync(async () => await _productRepository.UpsertAsync(entity));
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
