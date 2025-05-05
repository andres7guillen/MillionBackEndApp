using Microsoft.EntityFrameworkCore;
using MillionApp.Data.Context;
using MillionApp.Domain.Entities;
using MillionApp.Infrastructure.Repositories;
using Moq;

namespace MillionApp.Test;

[TestFixture]
public class PropertyRepositoryTests
{
    private ApplicationDbContext _context;
    private PropertyRepository _repository;

    [SetUp]
    public void SetUp()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // usar base nueva en cada test
            .Options;

        _context = new ApplicationDbContext(options);
        _repository = new PropertyRepository(_context);
    }

    [Test]
    public async Task GetByIdAsync_WhenPropertyExists_ReturnsSuccess()
    {
        // Arrange
        var property = Property.CreateProperty("House", "street 123", 15000, "mi2585", 2022, Guid.Parse("AD4C83F4-2510-46AA-9722-6387C7557089"));
        _context.Properties.Add(property.Value);
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.GetByIdAsync(property.Value.PropertyId);

        // Assert
        Assert.IsTrue(result.IsSuccess);
        Assert.AreEqual(property.Value.PropertyId, result.Value.PropertyId);
    }

    [Test]
    public async Task GetByIdAsync_WhenPropertyDoesNotExist_ReturnsFailure()
    {
        // Act
        var result = await _repository.GetByIdAsync(Guid.NewGuid());

        // Assert
        Assert.IsTrue(result.IsFailure);
        Assert.AreEqual("Property not found.", result.Error);
    }

    [TearDown]
    public void TearDown()
    {
        _context.Dispose();
    }
}
