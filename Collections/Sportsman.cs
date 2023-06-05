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
   
    public class Sportsman
    {
        [BsonId, BsonElement("_id"), BsonRepresentation(BsonType.Int32)]
        public int Id { get; set; }
        [BsonElement("last_name"), BsonRepresentation(BsonType.String)]
        public string Last_Name { get; set; }
        [BsonElement("first_name"), BsonRepresentation(BsonType.String)]
        public string First_Name { get; set; }
        [BsonElement("sport_type"), BsonRepresentation(BsonType.String)]
        public List<string> Sport_Type { get; set; } = new List<string>();
        [BsonElement("club_id"), BsonRepresentation(BsonType.Int32)]
        public int Club_Id { get; set; }
       
        [BsonElement("coach_id"), BsonRepresentation(BsonType.Int32)]
        public List<Int32> Coach_Id { get; set; } = new List<Int32>();
    }
}
