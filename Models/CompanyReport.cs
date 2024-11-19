using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace SolarSync_API.Models
{
    public class CompanyReport : BaseCompanyReport
    {
        /// <summary>
        /// Identificador único do relatório.
        /// </summary>
        [BsonId]
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        [Required(ErrorMessage = "O Id é obrigatório.")]
        public string Id { get; set; }

        /// <summary>
        /// Identificador da Empresa relacionada ao relatório (Chave Estrangeira).
        /// </summary>
        [BsonElement("company_id"), BsonRepresentation(BsonType.ObjectId)]
        [Required(ErrorMessage = "O ID da empresa é obrigatório.")]
        public string CompanyId { get; set; }


        /// <summary>
        /// Data de criação do relatório.
        /// </summary>
        [BsonElement("creation_date"), BsonRepresentation(BsonType.DateTime)]
        [Required(ErrorMessage = "A data de criação é obrigatória.")]
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;
    }
}