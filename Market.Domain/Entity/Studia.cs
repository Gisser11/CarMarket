using Market.Domain.Enum.Studia;

namespace Market.Domain.Entity;

public class Studia
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string City { get; set; }

    public DateTime? DataCreate { get; set; }

    public decimal MedianPrice { get; set; }

    public decimal Rating { get; set; }

    public TypeStudia TypeStudia { get; set; }

    public TypeAdvantages TypeAdvantages { get; set; }

    public List<Assortment> Assortments { get; set; }
    
    public List<Service> Services { get; set; }

}