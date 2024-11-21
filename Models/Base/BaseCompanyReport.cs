using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SolarSync_API.Models;

public class BaseCompanyReport
{
    /// <summary>
    /// Soluções propostas pela empresa.
    /// </summary>
    [BsonElement("solution"), BsonRepresentation(BsonType.String)]
    public string Solution { get; set; }
        
    /// <summary>
    /// Soluções propostas pela empresa.
    /// </summary>
    [BsonElement("covarege_area"), BsonRepresentation(BsonType.String)]
    public string CovaregeArea { get; set; }
}