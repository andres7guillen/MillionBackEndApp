using AutoMapper;
using CSharpFunctionalExtensions;
using MillionApp.Application.Commands;
using MillionApp.Domain.Dtos;
using MillionApp.Domain.Entities;
using MillionApp.Domain.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MillionApp.Test;

[TestFixture]
public class CreatePropertyCommandHandlerTests
{
    private Mock<IPropertyRepository> _repositoryMock;
    private IMapper _mapper;
    private CreatePropertyCommandHandler _handler;

    [SetUp]
    public void Setup()
    {
        _repositoryMock = new Mock<IPropertyRepository>();

        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Property, PropertyDto>();
        });

        _mapper = config.CreateMapper();
        _handler = new CreatePropertyCommandHandler(_repositoryMock.Object, _mapper);
    }

    [Test]
    public async Task Handle_ShouldCreatePropertySuccessfully()
    {
        // Arrange
        var dto = new PropertyDto
        {
            Name = "Casa Bonita",
            Address = "Calle Falsa 123",
            Price = 100000,
            CodeInternal = "ABC123",
            Year = 2020,
            OwnerId = Guid.NewGuid()
        };

        var entity = Property.CreateProperty(
            dto.Name, dto.Address, dto.Price, dto.CodeInternal, dto.Year, dto.OwnerId).Value;

        _repositoryMock.Setup(r => r.AddAsync(It.IsAny<Property>()))
            .ReturnsAsync(Result.Success(entity));

        var command = new CreatePropertyCommand { Property = dto };

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.IsTrue(result.IsSuccess);
        Assert.AreEqual(dto.Name, result.Value.Name);
        _repositoryMock.Verify(r => r.AddAsync(It.IsAny<Property>()), Times.Once);
    }

    [Test]
    public async Task Handle_ShouldReturnFailure_WhenEntityCreationFails()
    {
        // Arrange
        var dto = new PropertyDto
        {
            Name = "", // Nombre inválido
            Address = "Calle Falsa 123",
            Price = 100000,
            CodeInternal = "ABC123",
            Year = 2020,
            OwnerId = Guid.NewGuid()
        };

        var command = new CreatePropertyCommand { Property = dto };

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.IsTrue(result.IsFailure);
        Assert.AreEqual("Name is required", result.Error);
        _repositoryMock.Verify(r => r.AddAsync(It.IsAny<Property>()), Times.Never);
    }

}
