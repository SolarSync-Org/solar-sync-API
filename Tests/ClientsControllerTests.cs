using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SolarSync_API.Controllers;
using SolarSync_API.Models;
using SolarSync_API.Services;
using Xunit;

namespace SolarSync_API.Tests.Controllers
{
    public class ClientsControllerTests
    {
        private readonly Mock<ClientService> _mockService;
        private readonly ClientsController _controller;

        public ClientsControllerTests()
        {
            _mockService = new Mock<ClientService>();
            _controller = new ClientsController(_mockService.Object);
        }

        [Fact]
        public async Task GetClients_ShouldReturnListOfClients()
        {
            // Arrange
            var clients = new List<Client>
            {
                new Client { Id = "1", Name = "Client 1", CPF = "12345678901", Email = "client1@test.com" },
                new Client { Id = "2", Name = "Client 2", CPF = "09876543210", Email = "client2@test.com" }
            };
            _mockService.Setup(s => s.GetClientsAsync()).ReturnsAsync(clients);

            // Act
            var result = await _controller.GetClients();

            // Assert
            var okResult = result.Result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);

            var returnedClients = okResult.Value as List<Client>;
            returnedClients.Should().BeEquivalentTo(clients);
        }

        [Fact]
        public async Task GetClientById_ShouldReturnClient_WhenFound()
        {
            // Arrange
            var client = new Client { Id = "1", Name = "Client 1", CPF = "12345678901", Email = "client1@test.com" };
            _mockService.Setup(s => s.GetClientByIdAsync("1")).ReturnsAsync(client);

            // Act
            var result = await _controller.GetClientById("1");

            // Assert
            var okResult = result.Result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);

            var returnedClient = okResult.Value as Client;
            returnedClient.Should().BeEquivalentTo(client);
        }

        [Fact]
        public async Task GetClientById_ShouldReturnNotFound_WhenNotFound()
        {
            // Arrange
            _mockService.Setup(s => s.GetClientByIdAsync("99")).ReturnsAsync((Client)null);

            // Act
            var result = await _controller.GetClientById("99");

            // Assert
            var notFoundResult = result.Result as NotFoundResult;
            notFoundResult.Should().NotBeNull();
            notFoundResult.StatusCode.Should().Be(404);
        }

        [Fact]
        public async Task PostClient_ShouldReturnCreatedClient()
        {
            // Arrange
            var clientDto = new Client { Name = "New Client", CPF = "12345678901", Email = "newclient@test.com" };
            var createdClient = new Client { Id = "1", Name = "New Client", CPF = "12345678901", Email = "newclient@test.com" };
            _mockService.Setup(s => s.CreateClientAsync(clientDto)).ReturnsAsync(createdClient);

            // Act
            var result = await _controller.PostClient(clientDto);

            // Assert
            var createdAtActionResult = result.Result as CreatedAtActionResult;
            createdAtActionResult.Should().NotBeNull();
            createdAtActionResult.StatusCode.Should().Be(201);

            var returnedClient = createdAtActionResult.Value as Client;
            returnedClient.Should().BeEquivalentTo(createdClient);
        }

        [Fact]
        public async Task DeleteClient_ShouldReturnNoContent_WhenDeleted()
        {
            // Arrange
            _mockService.Setup(s => s.DeleteClientAsync("1")).ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteClient("1");

            // Assert
            var okResult = result as OkResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task DeleteClient_ShouldReturnNotFound_WhenNotDeleted()
        {
            // Arrange
            _mockService.Setup(s => s.DeleteClientAsync("99")).ReturnsAsync(false);

            // Act
            var result = await _controller.DeleteClient("99");

            // Assert
            var notFoundResult = result as NotFoundResult;
            notFoundResult.Should().NotBeNull();
            notFoundResult.StatusCode.Should().Be(404);
        }
    }
}
