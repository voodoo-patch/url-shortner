using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace UrlsDAL.Entities
{
    public class TinyUrl
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string ShortUrl { get; set; }
        public string OriginalUrl { get; set; }
        public string UserId { get; set; }
        public DateTime? ExpiresOn { get; set; }
    }
}