using System.ComponentModel.DataAnnotations;

namespace Market.Domain.Enum.Studia;

public enum TypeAdvantages
{
    [Display(Name = "Быстрое обслуживание")]
    QuicklyRepair = 0,

    [Display(Name = "Премиальное обслуживание")]
    PremiumService = 1,

    [Display(Name = "Бесплатные напитки")] FreeDrinks = 2
}