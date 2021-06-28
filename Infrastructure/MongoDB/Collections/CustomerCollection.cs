using MongoDB.Bson.Serialization.Attributes;

namespace Exemplo.Infrastructure.MongoDB.Collections
{
    [BsonIgnoreExtraElements]
    public sealed class CustomerCollection
    {
        [BsonElement]
        public int CustomerId { get; set; }

        [BsonElement]
        public string Name { get; set; }
        [BsonElement]
        public string Cpf { get; set; }
        [BsonElement]
        public int Age { get; set; }
    }
}