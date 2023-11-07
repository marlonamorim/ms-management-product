using MGM.MS.Management.Product.Infrastructure.Config;
using MGM.MS.Management.Product.Infrastructure.Contexts;
using MGM.MS.Management.Product.Infrastructure.Entities;
using MGM.MS.Management.Product.Infrastructure.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace MGM.MS.Management.Product.Infrastructure.Repositories
{
    internal class ProductRepository : IProductRepository
    {
        private readonly MongoContext _mongoContext;
        private readonly IMongoCollection<ProductEntity> _productCollection;

        public ProductRepository(MongoContext mongoContext,
            IOptions<DatabaseConfiguration> dbOptions)
        {
            _mongoContext = mongoContext;

            _productCollection = _mongoContext.Database.GetCollection<ProductEntity>(dbOptions.Value.ProductCollectionName);
        }

        public async Task DeleteAsync(string id)
            => await _productCollection.DeleteOneAsync(x => x.Id == id);

        public async Task<string?> GetDetailsAsync(string id)
        {
            var ret = await _productCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
            return ret?.Details ?? default;
        }

        public async Task<IEnumerable<ProductEntity>> ListByCategoryAsync(string categoryId)
            => await _productCollection.Find(x => x.CategoryId == categoryId).ToListAsync();

        public async Task UpsertAsync(ProductEntity entity)
        {
            var ret = await _productCollection.Find(x => x.Id == entity.Id).FirstOrDefaultAsync();

            if (ret is null)
                await _productCollection.InsertOneAsync(entity);
            else
                await _productCollection.ReplaceOneAsync(x => x.Id == entity.Id, entity);
        }
    }
}
