namespace MillionApp.Domain.Dtos;

public class PropertyDto
{
    public Guid PropertyId { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public double Price { get; set; }
    public string CodeInternal { get; set; }
    public int Year { get; set; }
    public Guid OwnerId { get; set; }
}
