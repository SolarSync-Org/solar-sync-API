using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SolarSync_API.Models
{
    public class Company : BaseCompany
    {
        /// <summary>
        /// Identificador único da empresa.
        /// </summary>
        [BsonId]
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        /// <summary>
        /// Data e hora de registro da empresa.
        /// </summary>
        [BsonElement("registration_date"), BsonRepresentation(BsonType.DateTime)]
        public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;
    }
}