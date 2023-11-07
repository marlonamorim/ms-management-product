using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MGM.MS.Management.Product.Infrastructure.Entities
{
    public class CategoryEntity
    {
        public CategoryEntity(string? id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; private set; }

        [BsonElement("name")]
        public string Name { get; private set; } = string.Empty;

        [BsonElement("description")]
        public string Description { get; private set; } = string.Empty;
    }
}
