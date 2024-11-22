using System.Collections.Generic;
using System.Threading.Tasks;
using SolarSync_API.Data;
using MongoDB.Driver;
using SolarSync_API.Models;
using SolarSync_API.DTO;

namespace SolarSync_API.Services
{
    public class ClientService
    {
        private readonly IMongoCollection<Client> _clients;
        private readonly ValidationService _validationService;

        public ClientService(MongoDbService mongoDbService, ValidationService validationService)
        {
            _clients = mongoDbService.Database?.GetCollection<Client>("clients");
            _validationService = validationService;
        }

        public async Task<IEnumerable<Client>> GetClientsAsync()
        {
            return await _clients.Find(FilterDefinition<Client>.Empty).ToListAsync();
        }

        public async Task<Client?> GetClientByIdAsync(string id)
        {
            var filter = Builders<Client>.Filter.Eq(x => x.Id, id);
            return await _clients.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<Client?> GetClientByCPFAsync(string cpf)
        {
            var filter = Builders<Client>.Filter.Eq(x => x.CPF, cpf);
            return await _clients.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<Client?> UpdateClientAsync(string id, UpdateClientDTO clientDto)
        {
            var client = await GetClientByIdAsync(id);
            if (client == null) return null;

            if (!string.IsNullOrEmpty(clientDto.Email))
            {
                if (!_validationService.IsValidEmail(clientDto.Email))
                    throw new ArgumentException("O e-mail informado não é válido.");
                
                var emailExists = await _clients.Find(Builders<Client>.Filter.Eq(c => c.Email, clientDto.Email)).FirstOrDefaultAsync();
                if (emailExists != null) throw new InvalidOperationException("O e-mail informado já está cadastrado. Por favor, use outro e-mail.");
            }

            if (!string.IsNullOrEmpty(clientDto.Name)) client.Name = clientDto.Name;
            if (!string.IsNullOrEmpty(clientDto.CPF)) client.CPF = clientDto.CPF;
            if (!string.IsNullOrEmpty(clientDto.Phone)) client.Phone = clientDto.Phone;
            if (!string.IsNullOrEmpty(clientDto.Email)) client.Email = clientDto.Email;
            if (clientDto.Address != null) client.Address = clientDto.Address;

            await _clients.ReplaceOneAsync(c => c.Id == id, client);
            return client;
        }

        public async Task<Client> CreateClientAsync(BaseClient clientDto)
        {

            var cpfExists = await _clients
                .Find(Builders<Client>.Filter.Eq(c => c.CPF, clientDto.CPF))
                .FirstOrDefaultAsync();
            if (cpfExists != null) throw new InvalidOperationException("O CPF informado já está cadastrado. Por favor, use outro CPF.");

            if (!_validationService.IsValidEmail(clientDto.Email))
            {
                throw new ArgumentException("O e-mail informado não é válido.");
            }

            var emailExists = await _clients
                .Find(Builders<Client>.Filter.Eq(c => c.Email, clientDto.Email))
                .FirstOrDefaultAsync();
            if (emailExists != null) throw new InvalidOperationException("O e-mail informado já está cadastrado. Por favor, use outro e-mail.");

            if (!await _validationService.IsEmailValidAsync(clientDto.Email))
            {
                throw new ArgumentException("O e-mail informado é considerado inválido pela API.");
            }

            var client = new Client()
            {
                Name = clientDto.Name,
                Address = clientDto.Address,
                CPF = clientDto.CPF,
                Email = clientDto.Email,
                Phone = clientDto.Phone
            };

            await _clients.InsertOneAsync(client);
            return client;
        }

        public async Task<bool> DeleteClientAsync(string id)
        {
            var filter = Builders<Client>.Filter.Eq(x => x.Id, id);
            var result = await _clients.DeleteOneAsync(filter);
            return result.DeletedCount > 0;
        }
    }
}
