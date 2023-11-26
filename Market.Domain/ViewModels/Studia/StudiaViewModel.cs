using System.Text.Json.Serialization;
using Market.Domain.Enum.Studia;

namespace Market.Domain.ViewModels.StudiaViewModel;

public class StudiaViewModel
{
    public int Id { get; set; }

    [JsonPropertyName("Name")] public string Name { get; set; }

    [JsonPropertyName("City")] public string City { get; set; }

    public DateTime DataCreate { get; set; }

    [JsonPropertyName("MedianPrice")] public decimal MedianPrice { get; set; }

    [JsonPropertyName("Rating")] public decimal Rating { get; set; }

    [JsonPropertyName("TypeStudia")] public TypeStudia TypeStudia { get; set; }

    [JsonPropertyName("TypeAdvantages")] public TypeAdvantages TypeAdvantages { get; set; }
    
   
}