using System.Collections.Generic;
using System.Threading.Tasks;
using SolarSync_API.Models;
using SolarSync_API.DTO;
using SolarSync_API.DTO.ClientReports;

namespace SolarSync_API.Interface
{
    public interface IClientReportService
    {
        Task<ClientReport> CreateAsync(ClientReportDTO clientReportDto);
        Task<ClientReport> GetByIdAsync(string id);
        Task<IEnumerable<ClientReport>> GetAllAsync();
        Task<ClientReport> UpdateAsync(string id, UpdateClientReportDTO clientReportDto);
        Task<bool> DeleteAsync(string id);
        Task UpdateLabelsAsync();

    }
}