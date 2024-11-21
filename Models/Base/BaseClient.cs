using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SolarSync_API.Models
{
    /// <summary>
    /// Classe base para representar as propriedades comuns de clientes.
    /// </summary>
    public class BaseClient
    {
        /// <summary>
        /// Nome do Cliente.
        /// </summary>
        [BsonElement("name"), BsonRepresentation(BsonType.String)]
        [Required(ErrorMessage = "O nome do cliente é obrigatório.")]
        public string Name { get; set; }

        /// <summary>
        /// Endereço do Cliente.
        /// </summary>
        [BsonElement("address")]
        [Required(ErrorMessage = "O endereço é obrigatório.")]
        public Address Address { get; set; }

        /// <summary>
        /// CPF do Cliente.
        /// </summary>
        [BsonElement("cpf"), BsonRepresentation(BsonType.String)]
        [Required(ErrorMessage = "O CPF é obrigatório.")]
        [MinLength(11, ErrorMessage = "O CPF deve ter 11 caracteres.")]
        [MaxLength(11, ErrorMessage = "O CPF deve ter 11 caracteres.")]
        public string CPF { get; set; }

        /// <summary>
        /// Renda mensal do Cliente.
        /// </summary>
        [BsonElement("rent"), BsonRepresentation(BsonType.String)]
        [Required(ErrorMessage = "A renda é obrigatória.")]
        public string Rent { get; set; }

        /// <summary>
        /// Email do Cliente.
        /// </summary>
        [BsonElement("email"), BsonRepresentation(BsonType.String)]
        [Required(ErrorMessage = "O email é obrigatório.")]
        [EmailAddress(ErrorMessage = "O email deve estar em um formato válido.")]
        public string Email { get; set; }

        /// <summary>
        /// Telefone do Cliente.
        /// </summary>
        [BsonElement("phone"), BsonRepresentation(BsonType.String)]
        [Required(ErrorMessage = "O telefone é obrigatório.")]
        [Phone(ErrorMessage = "O telefone deve estar em um formato válido.")]
        public string Phone { get; set; }
    }
}
