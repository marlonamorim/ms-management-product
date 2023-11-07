using MGM.MS.Management.Product.Infrastructure.Config;
using MGM.MS.Management.Product.Infrastructure.Contexts;
using MGM.MS.Management.Product.Infrastructure.Entities;
using MGM.MS.Management.Product.Infrastructure.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace MGM.MS.Management.Product.Infrastructure.Repositories
{
    internal class CategoryRepository : ICategoryRepository
    {
        private readonly MongoContext _mongoContext;
        private readonly IMongoCollection<CategoryEntity> _categoryCollection;

        public CategoryRepository(MongoContext mongoContext,
            IOptions<DatabaseConfiguration> dbOptions)
        {
            _mongoContext = mongoContext;

            _categoryCollection = _mongoContext.Database.GetCollection<CategoryEntity>(dbOptions.Value.CategoryCollectionName);
        }

        public async Task DeleteAsync(string id)
            => await _categoryCollection.DeleteOneAsync(x => x.Id == id);

        public async Task<IEnumerable<CategoryEntity>> ListAsync()
            => await _categoryCollection.Find(_ => true).ToListAsync();

        public async Task UpsertAsync(CategoryEntity entity)
        {
            var ret = await _categoryCollection.Find(x => x.Id == entity.Id).FirstOrDefaultAsync();

            if (ret is null)
                await _categoryCollection.InsertOneAsync(entity);
            else
                await _categoryCollection.ReplaceOneAsync(x => x.Id == entity.Id, entity);
        }
    }
}
