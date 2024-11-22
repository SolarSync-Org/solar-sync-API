using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using SolarSync_API.Domain.ML;
using SolarSync_API.Models;
using SolarSync_API.DTO;
using SolarSync_API.DTO.ClientReports;
using SolarSync_API.Interface;

namespace SolarSync_API.Services
{
    public class ClientReportService : IClientReportService
    {
        private readonly IMongoCollection<ClientReport> _collection;

        public ClientReportService(IMongoDatabase database)
        {
            _collection = database.GetCollection<ClientReport>("clientsReports");
        }

        public async Task<ClientReport> CreateAsync(ClientReportDTO clientReportDto)
        {
            var clientReport = MapToEntity(clientReportDto);
            await _collection.InsertOneAsync(clientReport);
            return clientReport;
        }

        public async Task<ClientReport> GetByIdAsync(string id)
        {
            return await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ClientReport>> GetAllAsync()
        {
            return await _collection.Find(x => true).ToListAsync();
        }

        public async Task<ClientReport> UpdateAsync(string id, UpdateClientReportDTO clientReportDto)
        {
            var clientReport = await GetByIdAsync(id);
            if (clientReport == null) return null;

            if (!string.IsNullOrEmpty(clientReportDto.ClientId)) clientReport.ClientId = clientReportDto.ClientId;
            if (!string.IsNullOrEmpty(clientReportDto.ResidenceType)) clientReport.ResidenceType = clientReportDto.ResidenceType;
            if (clientReportDto.Rent.HasValue) clientReport.Rent = clientReportDto.Rent.Value;
            if (clientReportDto.Potential.HasValue) clientReport.Potential = clientReportDto.Potential.Value;
            if (clientReportDto.EnergyConsumption.HasValue) clientReport.EnergyConsumption = clientReportDto.EnergyConsumption.Value;

            await _collection.ReplaceOneAsync(c => c.Id == id, clientReport);
            return clientReport;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var result = await _collection.DeleteOneAsync(x => x.Id == id);
            return result.IsAcknowledged && result.DeletedCount > 0;
        }

        private ClientReport MapToEntity(ClientReportDTO dto)
        {
            return new ClientReport
            {
                ClientId = dto.ClientId,
                ResidenceType = dto.ResidenceType,
                Rent = dto.Rent,
                Potential = dto.Potential,
                EnergyConsumption = dto.EnergyConsumption,
                Label = dto.Label,
                RegistrationDate = DateTime.UtcNow
            };
        }
        
        public async Task UpdateLabelsAsync()
        {
            var reports = await GetAllAsync();

            // Verificar se há dados suficientes com Label para o treinamento
            var labeledReports = reports.Where(r => !string.IsNullOrEmpty(r.Label)).ToList();
            if (!labeledReports.Any())
                throw new InvalidOperationException("Não há relatórios rotulados disponíveis para treinamento.");

            // Treinar o modelo
            var mlService = new ClientReportMLService();
            mlService.Train(labeledReports);

            // Atualizar os relatórios sem Label
            foreach (var report in reports.Where(r => string.IsNullOrEmpty(r.Label)))
            {
                var input = new ClientReportInput
                {
                    ResidenceType = report.ResidenceType,
                    Rent = report.Rent,
                    Potential = report.Potential,
                    EnergyConsumption = report.EnergyConsumption
                };

                report.Label = mlService.Predict(input);

                // Atualizar o relatório no banco
                await _collection.ReplaceOneAsync(r => r.Id == report.Id, report);
            }
        }




    }
}
