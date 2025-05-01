using CSharpFunctionalExtensions;

namespace MillionApp.Domain.Entities;

public class Owner
{
    public Guid OwnerId { get; private set; }
    public string Name { get; private set; }
    public string Address { get; private set; }
    public string Photo { get; private set; }
    public DateTime Birthday { get; private set; }

    public ICollection<Property> Properties { get; private set; } = new List<Property>();

    private Owner() { }

    public static Result<Owner> CreateOwner(string name, string address, string photo, DateTime birthday)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure<Owner>("Name is required");

        if (birthday > DateTime.UtcNow)
            return Result.Failure<Owner>("Birthday cannot be in the future");

        var owner = new Owner
        {
            OwnerId = Guid.NewGuid(),
            Name = name,
            Address = address,
            Photo = photo,
            Birthday = birthday
        };

        return Result.Success(owner);
    }

    public Result Update(string name, string address, string photo, DateTime birthday)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure("Name is required");

        if (birthday > DateTime.UtcNow)
            return Result.Failure("Birthday cannot be in the future");

        Name = name;
        Address = address;
        Photo = photo;
        Birthday = birthday;

        return Result.Success();
    }
}
