
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using SolarSync_API.Models;

namespace SolarSync_API.DTO;

public class CompanyReportDTO : BaseCompanyReport
{
    /// <summary>
    /// Identificador da Empresa relacionada ao relatório (Chave Estrangeira).
    /// </summary>
    [BsonElement("company_id"), BsonRepresentation(BsonType.ObjectId)]
    [Required(ErrorMessage = "O ID da empresa é obrigatório.")]
    public string CompanyId { get; set; }

}