using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace SolarSync_API.Models
{
    /// <summary>
    /// Soluções que a empresa disponibiliza.
    /// </summary>
    public class Solution
    {
        /// <summary>
        /// Tipo da solução.
        /// </summary>
        [BsonElement("type"), BsonRepresentation(BsonType.String)]
        [Required(ErrorMessage = "O tipo da solução é obrigatório.")]
        public string Type { get; set; }

        /// <summary>
        /// Descrição da solução.
        /// </summary>
        [BsonElement("description"), BsonRepresentation(BsonType.String)]
        [Required(ErrorMessage = "A descrição da solução é obrigatória.")]
        public string Description { get; set; }

        /// <summary>
        /// Área de cobertura da solução.
        /// </summary>
        [BsonElement("coverage_area"), BsonRepresentation(BsonType.String)]
        [Required(ErrorMessage = "A área de cobertura é obrigatória.")]
        public string CoverageArea { get; set; }
    }
}