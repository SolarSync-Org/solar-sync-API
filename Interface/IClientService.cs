using System.Collections.Generic;
using System.Threading.Tasks;
using SolarSync_API.Models;
using SolarSync_API.DTO;

namespace SolarSync_API.Services
{
    public interface IClientService
    {
        Task<IEnumerable<Client>> GetClientsAsync();
        Task<Client?> GetClientByIdAsync(string id);
        Task<Client?> GetClientByCPFAsync(string cpf);
        Task<Client?> UpdateClientAsync(string id, UpdateClientDTO clientDto);
        Task<Client> CreateClientAsync(BaseClient clientDto);
        Task<bool> DeleteClientAsync(string id);
    }
}