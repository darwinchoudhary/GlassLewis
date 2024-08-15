using System;
using GlassLewisWebAPI.Database;
using GlassLewisWebAPI.Models;
using GlassLewisWebAPI.Services.Implementations;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace GlassLewisWebAPI.UnitTests.Services;

public class CompanyServiceTests
{
    private CompanyService _companyService;
    private CompanyDbContext _context;

    public CompanyServiceTests()
    {
        var options = new DbContextOptionsBuilder<CompanyDbContext>()
                .UseInMemoryDatabase(databaseName: "CompanyDatabase")
                .Options;

        _context = new CompanyDbContext(options);
        _companyService = new CompanyService(_context);
    }

    [Fact]
    public async Task GetCompanyById_ReturnsCompany_WhenCompanyExists()
    {
        // Arrange
        var company = new Company { Id = 1, Name = "Test Company", Ticker = "TST", Isin = "US1234567890", Exchange = "NYSE" };
        var createResult = await _companyService.CreateCompany(company);

        // Act
        var result = await _companyService.GetCompanyById(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(company.Isin, result.Isin);
        Assert.Equal(company.Name, result.Name);
    }

    [Fact]
    public async Task GetCompanyById_ReturnsNull_WhenCompanyDoesNotExist()
    {
        // Act
        var result = await _companyService.GetCompanyById(999);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task CreateCompany_ReturnsSuccessTrue_WhenCompanyIsValid()
    {
        // Arrange
        var company = new Company { Id = 2, Name = "New Company", Ticker = "NEW", Isin = "US0987654321", Exchange = "NASDAQ" };

        // Act
        var result = await _companyService.CreateCompany(company);

        // Assert
        Assert.True(result.Success);
    }

    [Fact]
    public async Task CreateCompany_ReturnsSuccessFalse_WhenIsinIsDuplicate()
    {
        // Arrange
        var company1 = new Company { Id = 3, Name = "Company One", Ticker = "ONE", Isin = "US1234567891", Exchange = "NYSE" };
        var company2 = new Company { Id = 4, Name = "Company Two", Ticker = "TWO", Isin = "US1234567891", Exchange = "NASDAQ" };

        var x = await _companyService.CreateCompany(company1);

        // Act
        var result = await _companyService.CreateCompany(company2);

        // Assert
        Assert.False(result.Success);
        Assert.Equal("A company with the same ISIN already exists.", result.Error);
    }
}
