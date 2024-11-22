using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Microsoft.ML.Data;
using System;

namespace SolarSync_API.Models
{
    /// <summary>
    /// Representa o relatório do cliente, utilizado tanto para armazenamento quanto para Machine Learning.
    /// </summary>
    public class ClientReport : BaseClientReport
    {
        /// <summary>
        /// Identificador único do relatório.
        /// </summary>
        [BsonId]
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        /// <summary>
        /// Data e hora de registro do relatório.
        /// </summary>
        [BsonElement("registration_date"), BsonRepresentation(BsonType.DateTime)]
        public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;
        
        
    }
}
