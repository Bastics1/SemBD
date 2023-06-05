using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SemDBMongoDB
{
    [Serializable]
    internal class Coach
    {
        [BsonId, BsonElement("_id"), BsonRepresentation(BsonType.Int32)]
        public int Id { get; set; }
        [BsonElement("last_name"), BsonRepresentation(BsonType.String)]
        public string Last_Name { get; set; }
        [BsonElement("first_name"), BsonRepresentation(BsonType.String)]
        public string First_Name { get; set; }
        [BsonElement("sport_type"), BsonRepresentation(BsonType.String)]
        public string Sport_Type { get; set; }
        [BsonElement("sportsman_id"), BsonRepresentation(BsonType.Int32)]
        public List<Int32> Sportsman_Id { get; set; } = new List<Int32>();

    }
}
