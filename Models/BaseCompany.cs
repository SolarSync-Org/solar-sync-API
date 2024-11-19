using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SolarSync_API.Models
{
    /// <summary>
    /// Classe base para propriedades comuns de empresas.
    /// </summary>
    public class BaseCompany
    {
        /// <summary>
        /// Nome da Empresa.
        /// </summary>
        [BsonElement("name"), BsonRepresentation(BsonType.String)]
        [Required(ErrorMessage = "O nome da empresa é obrigatório.")]
        public string Name { get; set; }

        /// <summary>
        /// Descrição detalhada da Empresa.
        /// </summary>
        [BsonElement("description"), BsonRepresentation(BsonType.String)]
        [Required(ErrorMessage = "A descrição é obrigatória.")]
        public string Description { get; set; }
        
        /// <summary>
        /// CNPJ da Empresa.
        /// </summary>
        [BsonElement("cnpj"), BsonRepresentation(BsonType.String)]
        [Required(ErrorMessage = "O CNPJ é obrigatório."), MinLength(14, ErrorMessage = "O CNPJ deve ter 14 caracteres.")]
        public String CNPJ { get; set; }

        /// <summary>
        /// Email da Empresa. Deve conter o endereçio de email "ex: @xxx.xxx"
        /// </summary>
        [BsonElement("email"), BsonRepresentation(BsonType.String)]
        [Required(ErrorMessage = "O email é obrigatório.")]
        [EmailAddress(ErrorMessage = "O email deve estar em um formato válido.")]
        public String Email { get; set; }

        /// <summary>
        /// Status atual da Empresa.
        /// </summary>
        [BsonElement("status"), BsonRepresentation(BsonType.String)]
        [Required(ErrorMessage = "O status é obrigatório.")]
        public string Status { get; set; } = "Ativa";
    }
}