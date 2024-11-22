using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SolarSync_API.Controllers;
using SolarSync_API.DTO;
using SolarSync_API.Models;
using SolarSync_API.Interface;
using Xunit;

namespace SolarSync_API.Tests.Controllers
{
    public class CompanyReportControllerTests
    {
        private readonly Mock<ICompanyReportService> _mockService;
        private readonly CompanyReportController _controller;

        public CompanyReportControllerTests()
        {
            _mockService = new Mock<ICompanyReportService>();
            _controller = new CompanyReportController(_mockService.Object);
        }

        [Fact]
        public async Task GetAll_ShouldReturnListOfReports()
        {
            // Arrange
            var reports = new List<CompanyReport>
            {
                new CompanyReport { Id = "1", CompanyId = "1001", Solution = "Solution 1", CovaregeArea = "Area 1" },
                new CompanyReport { Id = "2", CompanyId = "1002", Solution = "Solution 2", CovaregeArea = "Area 2" }
            };
            _mockService.Setup(s => s.GetAllAsync()).ReturnsAsync(reports);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = result.Result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);

            var returnedReports = okResult.Value as IEnumerable<CompanyReport>;
            returnedReports.Should().BeEquivalentTo(reports);
        }

        [Fact]
        public async Task GetById_ShouldReturnReport_WhenFound()
        {
            // Arrange
            var report = new CompanyReport { Id = "1", CompanyId = "1001", Solution = "Solution 1", CovaregeArea = "Area 1" };
            _mockService.Setup(s => s.GetByIdAsync("1")).ReturnsAsync(report);

            // Act
            var result = await _controller.GetById("1");

            // Assert
            var okResult = result.Result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);

            var returnedReport = okResult.Value as CompanyReport;
            returnedReport.Should().BeEquivalentTo(report);
        }

        [Fact]
        public async Task GetById_ShouldReturnNotFound_WhenNotFound()
        {
            // Arrange
            _mockService.Setup(s => s.GetByIdAsync("99")).ReturnsAsync((CompanyReport)null);

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
            var reportDtos = new List<CompanyReportDTO>
            {
                new CompanyReportDTO { CompanyId = "1001", Solution = "Solution 1", CovaregeArea = "Area 1" },
                new CompanyReportDTO { CompanyId = "1002", Solution = "Solution 2", CovaregeArea = "Area 2" }
            };

            var createdReports = new List<CompanyReport>
            {
                new CompanyReport { Id = "1", CompanyId = "1001", Solution = "Solution 1", CovaregeArea = "Area 1" },
                new CompanyReport { Id = "2", CompanyId = "1002", Solution = "Solution 2", CovaregeArea = "Area 2" }
            };

            _mockService
                .SetupSequence(s => s.CreateAsync(It.IsAny<CompanyReportDTO>()))
                .ReturnsAsync(createdReports[0])
                .ReturnsAsync(createdReports[1]);

            // Act
            var result = await _controller.Create(reportDtos);

            // Assert
            var createdAtActionResult = result as CreatedAtActionResult;
            createdAtActionResult.Should().NotBeNull();
            createdAtActionResult.StatusCode.Should().Be(201);

            var returnedReports = createdAtActionResult.Value as IEnumerable<CompanyReport>;
            returnedReports.Should().BeEquivalentTo(createdReports);
        }

        [Fact]
        public async Task Update_ShouldReturnNoContent_WhenSuccessful()
        {
            // Arrange
            var updateDto = new UpdateCompanyReportDTO { Solution = "Updated Solution", CovaregeArea = "Updated Area" };
            _mockService.Setup(s => s.UpdateAsync("1", updateDto)).ReturnsAsync(new CompanyReport());

            // Act
            var result = await _controller.Update("1", updateDto);

            // Assert
            var noContentResult = result as NoContentResult;
            noContentResult.Should().NotBeNull();
            noContentResult.StatusCode.Should().Be(204);
        }

        [Fact]
        public async Task Update_ShouldReturnNotFound_WhenNotSuccessful()
        {
            // Arrange
            var updateDto = new UpdateCompanyReportDTO { Solution = "Updated Solution", CovaregeArea = "Updated Area" };
            _mockService.Setup(s => s.UpdateAsync("99", updateDto)).ReturnsAsync((CompanyReport)null);

            // Act
            var result = await _controller.Update("99", updateDto);

            // Assert
            var noContentResult = result as NoContentResult;
            noContentResult.Should().BeNull();

            var notFoundResult = result as NotFoundResult;
            notFoundResult.Should().NotBeNull();
            notFoundResult.StatusCode.Should().Be(404);
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
