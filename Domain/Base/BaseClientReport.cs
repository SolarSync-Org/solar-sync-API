using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SolarSync_API.Models;

public class BaseClientReport
{
    /// <summary>
    /// Identificador do cliente relacionado a este relatório.
    /// </summary>
    [BsonElement("client_id"), BsonRepresentation(BsonType.String)]
    public string ClientId { get; set; }

    /// <summary>
    /// Tipo de residência do cliente. Exemplo: apartamento, casa, fazenda.
    /// </summary>
    [BsonElement("residence_type"), BsonRepresentation(BsonType.String)]
    public string ResidenceType { get; set; }

    /// <summary>
    /// Valor do aluguel mensal pago pelo cliente, se aplicável.
    /// </summary>
    [BsonElement("rent"), BsonRepresentation(BsonType.Decimal128)]
    public float Rent { get; set; }

    /// <summary>
    /// Potencial do cliente para adoção de soluções propostas, variando de 0 a 1.
    /// </summary>
    [BsonElement("potential"), BsonRepresentation(BsonType.Double)]
    public float Potential { get; set; }

    /// <summary>
    /// Consumo médio mensal de energia elétrica do cliente, em kWh.
    /// </summary>
    [BsonElement("energy_consumption"), BsonRepresentation(BsonType.Double)]
    public float EnergyConsumption { get; set; }
    
    /// <summary>
    /// Grupo de solução identificado pelo modelo de IA.
    /// </summary>
    [BsonElement("label"), BsonRepresentation(BsonType.String)]
    public string Label { get; set; }
}