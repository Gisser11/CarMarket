using System.ComponentModel.DataAnnotations;

namespace Market.Domain.Enum.Studia;

public enum TypeStudia
{
    [Display(Name = "Автосервис")] AutoService = 0,

    [Display(Name = "Мойка машины")] WashCar = 1,

    [Display(Name = "Детейлинг")] Detailing = 2
}