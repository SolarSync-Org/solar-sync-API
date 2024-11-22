using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using SolarSync_API.Models;
using SolarSync_API.DTO;
using SolarSync_API.Interface;

namespace SolarSync_API.Services
{
    public class CompanyReportService : ICompanyReportService
    {
        private readonly IMongoCollection<CompanyReport> _collection;

        public CompanyReportService(IMongoDatabase database)
        {
            _collection = database.GetCollection<CompanyReport>("companiesReports");
        }

        public async Task<CompanyReport> CreateAsync(CompanyReportDTO companyReportDto)
        {
            var companyReport = MapToEntity(companyReportDto);
            await _collection.InsertOneAsync(companyReport);
            return companyReport;
        }

        public async Task<CompanyReport> GetByIdAsync(string id)
        {
            return await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<CompanyReport>> GetAllAsync()
        {
            return await _collection.Find(x => true).ToListAsync();
        }


        public async Task<CompanyReport> UpdateAsync(string id, UpdateCompanyReportDTO companyReportDto)
        {
            var companyReport = await GetByIdAsync(id);
            if (companyReport == null) return null;
            
            if (!string.IsNullOrEmpty(companyReportDto.CompanyId)) companyReport.CompanyId = companyReportDto.CompanyId;
            if (!string.IsNullOrEmpty(companyReportDto.Solution)) companyReport.Solution = companyReportDto.Solution;
            if (!string.IsNullOrEmpty(companyReportDto.Solution)) companyReport.CovaregeArea = companyReportDto.CovaregeArea;

            await _collection.ReplaceOneAsync(c => c.Id == id, companyReport);
            return companyReport;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var result = await _collection.DeleteOneAsync(x => x.Id == id);
            return result.IsAcknowledged && result.DeletedCount > 0;
        }

        private CompanyReport MapToEntity(CompanyReportDTO dto)
        {
            return new CompanyReport
            {
                CompanyId = dto.CompanyId,
                Solution = dto.Solution,
                CovaregeArea = dto.CovaregeArea,
            };
        }
        
    }
}
