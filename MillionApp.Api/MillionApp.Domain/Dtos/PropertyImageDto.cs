namespace MillionApp.Domain.Dtos;

public class PropertyImageDto
{
    public Guid PropertyImageId { get; set; }
    public Guid PropertyId { get; set; }
    public string File { get; set; }
    public bool Enabled { get; set; }
}
