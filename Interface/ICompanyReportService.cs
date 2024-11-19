using SolarSync_API.DTO;
using SolarSync_API.Models;

namespace SolarSync_API.Interface;

public interface ICompanyReportService
{
    Task<CompanyReport> CreateAsync(CompanyReportDTO campaignReportDto);
    Task<CompanyReport> GetByIdAsync(string id);
    Task<IEnumerable<CompanyReport>> GetAllAsync();
    Task<CompanyReport> UpdateAsync(string id, UpdateCompanyReportDTO companyReportDto);
    Task<bool> DeleteAsync(string id);
}