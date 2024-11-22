using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SolarSync_API.Controllers;
using SolarSync_API.DTO.ClientReports;
using SolarSync_API.Interface;
using SolarSync_API.Models;
using Xunit;
using FluentAssertions;

namespace SolarSync_API.Tests.Controllers
{
    public class ClientReportControllerTests
    {
        private readonly Mock<IClientReportService> _mockService;
        private readonly ClientReportController _controller;

        public ClientReportControllerTests()
        {
            _mockService = new Mock<IClientReportService>();
            _controller = new ClientReportController(_mockService.Object);
        }

        [Fact]
        public async Task GetAll_ShouldReturnListOfReports()
        {
            // Arrange
            var reports = new List<ClientReport>
            {
                new ClientReport { Id = "1", ResidenceType = "Casa", Rent = 1000, Potential = 0.8f, EnergyConsumption = 200 },
                new ClientReport { Id = "2", ResidenceType = "Apartamento", Rent = 800, Potential = 0.5f, EnergyConsumption = 150 }
            };
            _mockService.Setup(s => s.GetAllAsync()).ReturnsAsync(reports);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = result.Result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);

            var returnedReports = okResult.Value as IEnumerable<ClientReport>;
            returnedReports.Should().BeEquivalentTo(reports);
        }

        [Fact]
        public async Task GetById_ShouldReturnReport_WhenFound()
        {
            // Arrange
            var report = new ClientReport { Id = "1", ResidenceType = "Casa", Rent = 1000, Potential = 0.8f, EnergyConsumption = 200 };
            _mockService.Setup(s => s.GetByIdAsync("1")).ReturnsAsync(report);

            // Act
            var result = await _controller.GetById("1");

            // Assert
            var okResult = result.Result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);

            var returnedReport = okResult.Value as ClientReport;
            returnedReport.Should().BeEquivalentTo(report);
        }

        [Fact]
        public async Task GetById_ShouldReturnNotFound_WhenNotFound()
        {
            // Arrange
            _mockService.Setup(s => s.GetByIdAsync("99")).ReturnsAsync((ClientReport)null);

            // Act
            var result = await _controller.GetById("99");

            // Assert
            var notFoundResult = result.Result as NotFoundResult;
            notFoundResult.Should().NotBeNull();
            notFoundResult.StatusCode.Should().Be(404);
        }

        [Fact]
        public async Task Create_ShouldReturnCreatedReports()
        {
            // Arrange
            var reportsDto = new List<ClientReportDTO>
            {
                new ClientReportDTO { ResidenceType = "Casa", Rent = 1000, Potential = 0.8f, EnergyConsumption = 200 },
                new ClientReportDTO { ResidenceType = "Apartamento", Rent = 800, Potential = 0.5f, EnergyConsumption = 150 }
            };

            var createdReports = new List<ClientReport>
            {
                new ClientReport { Id = "1", ResidenceType = "Casa", Rent = 1000, Potential = 0.8f, EnergyConsumption = 200 },
                new ClientReport { Id = "2", ResidenceType = "Apartamento", Rent = 800, Potential = 0.5f, EnergyConsumption = 150 }
            };

            _mockService
                .SetupSequence(s => s.CreateAsync(It.IsAny<ClientReportDTO>()))
                .ReturnsAsync(createdReports[0])
                .ReturnsAsync(createdReports[1]);

            // Act
            var result = await _controller.Create(reportsDto);

            // Assert
            var createdAtActionResult = result as CreatedAtActionResult;
            createdAtActionResult.Should().NotBeNull();
            createdAtActionResult.StatusCode.Should().Be(201);

            var returnedReports = createdAtActionResult.Value as IEnumerable<ClientReport>;
            returnedReports.Should().BeEquivalentTo(createdReports);
        }

        [Fact]
        public async Task Delete_ShouldReturnNoContent_WhenSuccessful()
        {
            // Arrange
            _mockService.Setup(s => s.DeleteAsync("1")).ReturnsAsync(true);

            // Act
            var result = await _controller.Delete("1");

            // Assert
            var noContentResult = result as NoContentResult;
            noContentResult.Should().NotBeNull();
            noContentResult.StatusCode.Should().Be(204);
        }

        [Fact]
        public async Task Delete_ShouldReturnNotFound_WhenNotSuccessful()
        {
            // Arrange
            _mockService.Setup(s => s.DeleteAsync("99")).ReturnsAsync(false);

            // Act
            var result = await _controller.Delete("99");

            // Assert
            var notFoundResult = result as NotFoundResult;
            notFoundResult.Should().NotBeNull();
            notFoundResult.StatusCode.Should().Be(404);
        }
    }
}
