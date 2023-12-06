
namespace Market.Domain.ViewModels.User;

public class UserRegisterViewModel
{
    public string Name { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }
    
    public bool? TypeUserRole { get; set; }
}