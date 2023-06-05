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
    internal class Tournament
    {
        [BsonId, BsonElement("_id"), BsonRepresentation(BsonType.Int32)]
        public int Id { get; set; }
        [BsonElement("name"), BsonRepresentation(BsonType.String)]
        public string Name { get; set; }
        [BsonElement("organizator"), BsonRepresentation(BsonType.String)]
        public string Organizator { get; set; }
        [BsonElement("sport_type"), BsonRepresentation(BsonType.String)]
        public string Sport_Type { get; set; }
        [BsonElement("date"), BsonRepresentation(BsonType.DateTime)]
        public DateTime Date { get; set; }
        [BsonElement("sport_location_id"), BsonRepresentation(BsonType.Int32)]
        public int Sport_Location_Id { get; set; }
        [BsonElement("sportsman_places")]
        public BsonDocument Sportsman_Places { get; set; }
    }
}
