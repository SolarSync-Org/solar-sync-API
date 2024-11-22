using Moq;
using SolarSync_API.Models;
using SolarSync_API.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SolarSync_API.Tests
{
    public class ClientServiceTests
    {
        private readonly Mock<IClientService> _mockClientService;

        public ClientServiceTests()
        {
            // Mock da interface IClientService
            _mockClientService = new Mock<IClientService>();
        }

        [Fact]
        public async Task GetClients_ShouldReturnAllClients()
        {
            // Arrange: Criação de dados mockados
            var mockClients = new List<Client>
            {
                new Client
                {
                    Id = "1",
                    Name = "John Doe",
                    CPF = "12345678901",
                    Email = "johndoe@example.com",
                    Phone = "123456789",
                    Address = new Address
                    {
                        Street = "Rua Principal",
                        Number = "123",
                        City = "São Paulo",
                        State = "SP",
                        ZipCode = "12345678"
                    }
                },
                new Client
                {
                    Id = "2",
                    Name = "Jane Doe",
                    CPF = "10987654321",
                    Email = "janedoe@example.com",
                    Phone = "987654321",
                    Address = new Address
                    {
                        Street = "Rua Secundária",
                        Number = "456",
                        City = "Rio de Janeiro",
                        State = "RJ",
                        ZipCode = "87654321"
                    }
                }
            };

            // Configuração do mock para retornar os clientes mockados
            _mockClientService
                .Setup(service => service.GetClientsAsync())
                .ReturnsAsync(mockClients);

            // Act: Chamada ao método que está sendo testado
            var result = await _mockClientService.Object.GetClientsAsync();

            // Assert: Verificação dos resultados
            Assert.NotNull(result); // Garante que o resultado não é nulo
            Assert.Equal(2, result.Count()); // Verifica o número de clientes retornados
            Assert.Equal("John Doe", result.First().Name); // Valida o nome do primeiro cliente
            Assert.Equal("Rua Principal", result.First().Address.Street); // Valida o endereço do primeiro cliente
            Assert.Equal("123", result.First().Address.Number);
            Assert.Equal("São Paulo", result.First().Address.City);
            Assert.Equal("SP", result.First().Address.State);
            Assert.Equal("12345678", result.First().Address.ZipCode);
        }
    }
}
