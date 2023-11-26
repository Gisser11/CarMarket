using Market.Domain.Enum.UserRoles;

namespace Market.Domain.ViewModels.User;

public class UserViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public TypeUserRoles TypeUserRole { get; set; }
}