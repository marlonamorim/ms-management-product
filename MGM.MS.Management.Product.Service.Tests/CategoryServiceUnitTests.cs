using MGM.MS.Management.Product.Infrastructure.Entities;
using MGM.MS.Management.Product.Infrastructure.Interfaces;
using MGM.MS.Management.Product.Services.Dtos;
using MGM.MS.Management.Product.Services.Interfaces;
using MGM.MS.Management.Product.Services.Services;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using Moq;

namespace MGM.MS.Management.Product.Service.Tests
{
    public class CategoryServiceUnitTests
    {
        private readonly MockRepository _mockRepository;
        private readonly Mock<ICategoryRepository> _categoryRepository;
        private readonly ICategoryService _categoryService;

        public CategoryServiceUnitTests()
        {
            _mockRepository = new MockRepository(MockBehavior.Loose);
            _categoryRepository = _mockRepository.Create<ICategoryRepository>();

            using var logFactory = LoggerFactory.Create(builder => builder.AddConsole());
            var logger = logFactory.CreateLogger<CategoryService>();

            _categoryService = new CategoryService(_categoryRepository.Object, new Notification.Services.Notification(), logger);
        }

        [Fact]
        public async Task WhenListAllAsync_ShouldReturnCollectionService()
        {
            //Arr
            var dto = new CategoryDto { Id = ObjectId.GenerateNewId().ToString(), Name = "Teste", Description = "Teste" };
            var entity = new CategoryEntity(dto.Id, dto.Name, dto.Description);
            var list = new List<CategoryEntity> { entity };

            _categoryRepository
                .Setup(c => c.ListAsync())
                .ReturnsAsync(() => list);

            //Act
            var ret = await _categoryService.ListAllAsync();

            //Assert
            _categoryRepository.Verify(c => c.ListAsync(), Times.Once);
            Assert.NotNull(ret);
            Assert.True(ret.Any());
        }

        [Fact]
        public void WhenDeleteAsync_ShouldConsumeDeleteService()
        {
            //Arr
            var id = ObjectId.GenerateNewId().ToString();
            _categoryRepository.Setup(c => c.DeleteAsync(It.IsAny<string>()));

            //Act
            _categoryService.DeleteAsync(id);

            //Assert
            _categoryRepository.Verify(c => c.DeleteAsync(id), Times.Once());
        }
    }
}