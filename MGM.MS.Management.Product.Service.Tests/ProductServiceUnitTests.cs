using MGM.MS.Management.Product.Infrastructure.Entities;
using MGM.MS.Management.Product.Infrastructure.Interfaces;
using MGM.MS.Management.Product.Services.Interfaces;
using MGM.MS.Management.Product.Services.Services;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using Moq;

namespace MGM.MS.Management.Product.Service.Tests
{
    public class ProductServiceUnitTests
    {
        private readonly MockRepository _mockRepository;
        private readonly Mock<IProductRepository> _productRepository;
        private readonly IProductService _productService;

        public ProductServiceUnitTests()
        {
            _mockRepository = new MockRepository(MockBehavior.Loose);
            _productRepository = _mockRepository.Create<IProductRepository>();

            using var logFactory = LoggerFactory.Create(builder => builder.AddConsole());
            var logger = logFactory.CreateLogger<ProductService>();

            _productService = new ProductService(_productRepository.Object, new Notification.Services.Notification(), logger);
        }

        [Fact]
        public async Task WhenListByCategoryAsync_ShouldReturnCollectionService()
        {
            //Arr
            var entity = new ProductEntity(ObjectId.GenerateNewId().ToString(), "Teste", "Teste", 10.000m, "", ObjectId.GenerateNewId().ToString());
            var list = new List<ProductEntity> { entity };

            _productRepository
                .Setup(c => c.ListByCategoryAsync(It.IsAny<string>()))
                .ReturnsAsync(() => list);

            //Act
            var ret = await _productService.ListByCategoryAsync(entity.CategoryId);

            //Assert
            _productRepository.Verify(c => c.ListByCategoryAsync(entity.CategoryId), Times.Once);
            Assert.NotNull(ret);
            Assert.True(ret.Any());
        }

        [Fact]
        public void WhenDeleteAsync_ShouldConsumeDeleteService()
        {
            //Arr
            var id = ObjectId.GenerateNewId().ToString();
            _productRepository.Setup(c => c.DeleteAsync(It.IsAny<string>()));

            //Act
            _productService.DeleteAsync(id);

            //Assert
            _productRepository.Verify(c => c.DeleteAsync(id), Times.Once());
        }
    }
}
