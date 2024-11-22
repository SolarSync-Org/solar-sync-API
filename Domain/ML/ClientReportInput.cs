using Microsoft.ML.Data;

namespace SolarSync_API.Domain.ML;
public class ClientReportInput
{
    [LoadColumn(0)]
    public string ResidenceType { get; set; }

    [LoadColumn(1)]
    public float Rent { get; set; }

    [LoadColumn(2)]
    public float Potential { get; set; }

    [LoadColumn(3)]
    public float EnergyConsumption { get; set; }

    [LoadColumn(4)]
    [ColumnName("Label")] // Nome padrão exigido pelo ML.NET
    public string Label { get; set; }
}
