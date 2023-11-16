namespace Market.Domain.Entity;

public class Assortment
{
    public int Id { get; set; }

    public int AssortmentId { get; set; }

    public string Name { get; set; }

    public string Price { get; set; }
    
    public Studia Studia { get; set; }
}