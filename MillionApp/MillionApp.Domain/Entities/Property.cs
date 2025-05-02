using CSharpFunctionalExtensions;

namespace MillionApp.Domain.Entities;

public class Property
{
    public Guid PropertyId { get; private set; }
    public string Name { get; private set; }
    public string Address { get; private set; }
    public double Price { get; private set; }
    public string CodeInternal { get; private set; }
    public int Year { get; private set; }
    public Guid OwnerId { get; private set; }

    public Owner Owner { get; private set; }
    public ICollection<PropertyTrace> PropertyTraces { get; private set; } = new List<PropertyTrace>();
    public ICollection<PropertyImage> PropertyImages { get; private set; } = new List<PropertyImage>();

    private Property() { }

    public static Result<Property> CreateProperty(string name, string address, double price, string codeInternal, int year, Guid ownerId)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure<Property>("Name is required");

        if (string.IsNullOrWhiteSpace(address))
            return Result.Failure<Property>("Address is required");

        if (price <= 0)
            return Result.Failure<Property>("Price must be positive");

        var property = new Property
        {
            PropertyId = Guid.NewGuid(),
            Name = name,
            Address = address,
            Price = price,
            CodeInternal = codeInternal,
            Year = year,
            OwnerId = ownerId
        };

        return Result.Success(property);
    }
    public Result ChangePrice(double newPrice)
    {
        if (newPrice <= 0)
            return Result.Failure("The new price must be greater than zero.");

        Price = newPrice;
        return Result.Success();
    }

    public Result Update(string name, string address, double price, string codeInternal, int year, Guid ownerId)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure("Name is required.");

        if (string.IsNullOrWhiteSpace(address))
            return Result.Failure("Address is required.");

        if (price <= 0)
            return Result.Failure("Price must be greater than zero.");

        Name = name;
        Address = address;
        Price = price;
        CodeInternal = codeInternal;
        Year = year;
        OwnerId = ownerId;

        return Result.Success();
    }

}
