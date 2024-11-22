namespace SolarSync_API.DTO.ClientReports;

public class UpdateClientReportDTO
{
    public string? ClientId { get; set; }
    public string? ResidenceType { get; set; }
    public float? Rent { get; set; }
    public float? Potential { get; set; }
    public float? EnergyConsumption { get; set; }
}