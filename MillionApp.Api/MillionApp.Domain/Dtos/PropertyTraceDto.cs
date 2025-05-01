namespace MillionApp.Domain.Dtos;

public class PropertyTraceDto
{
    public Guid PropertyTraceId { get; set; }
    public DateTime DateSale { get; set; }
    public string Name { get; set; }
    public double Value { get; set; }
    public double Tax { get; set; }
    public Guid PropertyId { get; set; }
}
