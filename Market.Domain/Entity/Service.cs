namespace Market.Domain.Entity;

public class Service
{
    public int Id { get; set; }

    public int ServicesId { get; set; }

    public string Name { get; set; }

    public string ServiceType { get; set; }

    public string Price { get; set; }

    public string Description { get; set; }

    public Studia Studia { get; set; }
}