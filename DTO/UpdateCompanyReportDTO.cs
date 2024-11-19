using SolarSync_API.Models;

namespace SolarSync_API.DTO;

public class UpdateCompanyReportDTO : BaseCompanyReport
{
    /// <summary>
    /// Propriedades opcionais para atualização.
    /// </summary>
    public new string? CompanyId { get; set; }
    public new string? Solution { get; set; }
    public new string? CovaregeArea { get; set; }
}