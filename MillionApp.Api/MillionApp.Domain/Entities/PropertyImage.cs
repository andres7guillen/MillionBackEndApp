using CSharpFunctionalExtensions;

namespace MillionApp.Domain.Entities;

public class PropertyImage
{
    public Guid PropertyImageId { get; private set; }
    public Guid PropertyId { get; private set; }
    public string File { get; private set; }
    public bool Enabled { get; private set; }

    public Property Property { get; private set; }

    private PropertyImage() { }

    public static Result<PropertyImage> CreatePropertyImage(Guid propertyId, string file, bool enabled)
    {
        if (string.IsNullOrWhiteSpace(file))
            return Result.Failure<PropertyImage>("File is required");

        var image = new PropertyImage
        {
            PropertyImageId = Guid.NewGuid(),
            PropertyId = propertyId,
            File = file,
            Enabled = enabled
        };

        return Result.Success(image);
    }
    public Result Update(string file, bool enabled)
    {
        if (string.IsNullOrWhiteSpace(file))
            return Result.Failure("File is required");

        File = file;
        Enabled = enabled;

        return Result.Success();
    }
    public void Disable()
    {
        Enabled = false;
    }
}

