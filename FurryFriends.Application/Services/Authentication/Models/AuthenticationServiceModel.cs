namespace FurryFriends.Application.Services.Authentication.Models;

public class CredentialsModel
{
    public string Username { get; set; }
    public string Password { get; set; }
}

public class RegisterUserModel
{
    public string IdentityCard { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string CellPhone { get; set; }
    public string Address { get; set; }
    public DateTime Birthday { get; set; }
    public string Gender { get; set; }
}

public class ChangePasswordModel
{
    public int UserId { get; set; }
    public string OldPassword { get; set; }
    public string NewPassword { get; set; }
    public string ConfirmNewPassword { get; set; }
}

public class AuthenticatedUserModel
{
    public int UserId { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public int PersonId { get; set; }
    public int PersonTypeId { get; set; }
    public string PersonType { get; set; }
    public int PersonRoleId { get; set; }
    public string PersonRole { get; set; }
    public string IdentityCard { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string FullName => $"{Name} {Surname}";
    public int RoleId { get; set; }
    public string RoleName { get; set; }

}