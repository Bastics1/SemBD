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
    public class SportClub
    {
        [BsonId, BsonElement("_id"), BsonRepresentation(BsonType.Int32)]
        public int Id { get; set; }

        [BsonElement("name"), BsonRepresentation(BsonType.String)]
        public string Name { get; set; }

    }
}
