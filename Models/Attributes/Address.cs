using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SolarSync_API.Models;

/// <summary>
/// Classe para representar o endereço do cliente.
/// </summary>
public class Address
{
    /// <summary>
    /// Rua do Endereço.
    /// </summary>
    [BsonElement("street"), BsonRepresentation(BsonType.String)]
    [Required(ErrorMessage = "A rua é obrigatória.")]
    public string Street { get; set; }

    /// <summary>
    /// Número do Endereço.
    /// </summary>
    [BsonElement("number"), BsonRepresentation(BsonType.String)]
    [Required(ErrorMessage = "O número é obrigatório.")]
    public string Number { get; set; }

    /// <summary>
    /// Cidade do Endereço.
    /// </summary>
    [BsonElement("city"), BsonRepresentation(BsonType.String)]
    [Required(ErrorMessage = "A cidade é obrigatória.")]
    public string City { get; set; }

    /// <summary>
    /// Estado do Endereço.
    /// </summary>
    [BsonElement("state"), BsonRepresentation(BsonType.String)]
    [Required(ErrorMessage = "O estado é obrigatório.")]
    public string State { get; set; }

    /// <summary>
    /// CEP do Endereço.
    /// </summary>
    [BsonElement("zipCode"), BsonRepresentation(BsonType.String)]
    [Required(ErrorMessage = "O CEP é obrigatório.")]
    [MinLength(8, ErrorMessage = "O CEP deve ter pelo menos 8 caracteres.")]
    public string ZipCode { get; set; }
}