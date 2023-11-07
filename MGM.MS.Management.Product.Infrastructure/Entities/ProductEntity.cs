using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MGM.MS.Management.Product.Infrastructure.Entities
{
    public class ProductEntity
    {
        public ProductEntity(string? id, string name, string description, decimal price, string details, string categoryId)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            Details = details;
            CategoryId = categoryId;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; private set; }

        [BsonElement("name")]
        public string Name { get; private set; } = string.Empty;

        [BsonElement("description")]
        public string Description { get; private set; } = string.Empty;

        [BsonElement("price")]
        public decimal Price { get; private set; }

        [BsonElement("details")]
        public string Details { get; private set; } = string.Empty;

        [BsonElement("categoryId")]
        public string CategoryId { get; private set; }
    }
}
