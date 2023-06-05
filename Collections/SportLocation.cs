using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Serializers;

namespace SemDBMongoDB
{
    [Serializable]
    internal class SportLocation

    {
         
        [BsonId, BsonElement("_id"), BsonRepresentation(BsonType.Int32)]
        public int Id { get; set; }
        [BsonElement("name"), BsonRepresentation(BsonType.String)]
        public string Name { get; set; }
        [BsonElement("type"), BsonRepresentation(BsonType.String)]
        public string Type { get; set; }
        [BsonElement("property")]
        public BsonDocument Property { get; set; }
    }
}
