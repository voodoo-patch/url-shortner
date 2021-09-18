using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace KeysDAL.Entities
{
    public class FreshKey
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Key { get; set; }
    }
}