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
    public class CompaniesControllerTests
    {
        private readonly Mock<CompanyService> _mockService;
        private readonly CompaniesController _controller;

        public CompaniesControllerTests()
        {
            _mockService = new Mock<CompanyService>();
            _controller = new CompaniesController(_mockService.Object);
        }

        [Fact]
        public async Task GetCampaigns_ShouldReturnListOfCompanies()
        {
            // Arrange
            var companies = new List<Company>
            {
                new Company { Id = "1", Name = "Company 1", CNPJ = "12345678000101", Email = "company1@test.com" },
                new Company { Id = "2", Name = "Company 2", CNPJ = "98765432000109", Email = "company2@test.com" }
            };
            _mockService.Setup(s => s.GetCompaniesAsync()).ReturnsAsync(companies);

            // Act
            var result = await _controller.GetCampaigns();

            // Assert
            var okResult = result.Result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);

            var returnedCompanies = okResult.Value as List<Company>;
            returnedCompanies.Should().BeEquivalentTo(companies);
        }

        [Fact]
        public async Task GetCampaignById_ShouldReturnCompany_WhenFound()
        {
            // Arrange
            var company = new Company { Id = "1", Name = "Company 1", CNPJ = "12345678000101", Email = "company1@test.com" };
            _mockService.Setup(s => s.GetCompanyByIdAsync("1")).ReturnsAsync(company);

            // Act
            var result = await _controller.GetCampaignById("1");

            // Assert
            var okResult = result.Result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);

            var returnedCompany = okResult.Value as Company;
            returnedCompany.Should().BeEquivalentTo(company);
        }

        [Fact]
        public async Task GetCampaignById_ShouldReturnNotFound_WhenNotFound()
        {
            // Arrange
            _mockService.Setup(s => s.GetCompanyByIdAsync("99")).ReturnsAsync((Company)null);

            // Act
            var result = await _controller.GetCampaignById("99");

            // Assert
            var notFoundResult = result.Result as NotFoundResult;
            notFoundResult.Should().NotBeNull();
            notFoundResult.StatusCode.Should().Be(404);
        }

        [Fact]
        public async Task DeleteCompany_ShouldReturnNoContent_WhenDeleted()
        {
            // Arrange
            _mockService.Setup(s => s.DeleteCompanyAsync("1")).ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteCompany("1");

            // Assert
            var okResult = result as OkResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task DeleteCompany_ShouldReturnNotFound_WhenNotDeleted()
        {
            // Arrange
            _mockService.Setup(s => s.DeleteCompanyAsync("99")).ReturnsAsync(false);

            // Act
            var result = await _controller.DeleteCompany("99");

            // Assert
            var notFoundResult = result as NotFoundResult;
            notFoundResult.Should().NotBeNull();
            notFoundResult.StatusCode.Should().Be(404);
        }
    }
}
