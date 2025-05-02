namespace MillionApp.Domain.Dtos;

public class OwnerDto
{
    public Guid OwnerId { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string Photo { get; set; }
    public DateTime Birthday { get; set; }
}
