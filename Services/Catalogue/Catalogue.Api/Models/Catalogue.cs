using MongoDB.Bson.Serialization.Attributes;

namespace Catalogue.Api.Models
{
    public class Catalogue
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? Id { get; set; }
        public string? ProductName { get; set; }
        public int Cost { get; set; }
        public string? ImageUrl { get; set; }
        public string? Description { get; set; }
        public DateTime? ManufacturedOn { get; set; }
        public string? ManufacturedIn { get; set; }
    }
}
