using CSharpFunctionalExtensions;

namespace MillionApp.Domain.Entities;

public class PropertyTrace
{
    public Guid PropertyTraceId { get; private set; }
    public DateTime DateSale { get; private set; }
    public string Name { get; private set; }
    public double Value { get; private set; }
    public double Tax { get; private set; }
    public Guid PropertyId { get; private set; }

    public Property Property { get; private set; }

    private PropertyTrace() { }

    public static Result<PropertyTrace> CreatePropertyTrace(DateTime dateSale, string name, double value, double tax, Guid propertyId)
    {
        if (value <= 0)
            return Result.Failure<PropertyTrace>("Value must be positive");

        if (tax < 0)
            return Result.Failure<PropertyTrace>("Tax cannot be negative");

        var trace = new PropertyTrace
        {
            PropertyTraceId = Guid.NewGuid(),
            DateSale = dateSale,
            Name = name,
            Value = value,
            Tax = tax,
            PropertyId = propertyId
        };

        return Result.Success(trace);
    }
    public Result Update(DateTime dateSale, string name, double value, double tax)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure("Name is required");

        if (value <= 0)
            return Result.Failure("Value must be positive");

        if (tax < 0)
            return Result.Failure("Tax cannot be negative");

        DateSale = dateSale;
        Name = name;
        Value = value;
        Tax = tax;

        return Result.Success();
    }

}

