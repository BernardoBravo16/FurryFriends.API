namespace FurryFriends.Application.Services.Models;

public class UserModel
{
    public int UserId { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public int RoleId { get; set; }
    public string RoleName { get; set; }
    public bool UserStatus { get; set; }
    public int PersonId { get; set; }
    public int PersonTypeId { get; set; }
    public string PersonType { get; set; }
    public int PersonRoleId { get; set; }
    public string PersonRole { get; set; }
    public string IdentityCard { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string FullName => $"{Name} {Surname}";
    public string Cellphone { get; set; }
    public string Address { get; set; }
    public DateTime Birthday { get; set; }
    public string Gender { get; set; }
}

public class CreateUserModel
{
    public string IdentityCard { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string PersonType { get; set; }
    public string PersonRole { get; set; }
    public string Email { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
    public string Cellphone { get; set; }
    public string Address { get; set; }
    public DateTime Birthday { get; set; }
    public string Gender { get; set; }
}

public class CreateUserOnlyModel
{
    public int PersonId { get; set; }
    public int RoleId { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public bool Status { get; set; }
}

public class UpdateUserModel
{
    public int UserId { get; set; }
    public string Role { get; set; }
    public bool UserStatus { get; set; }
    public string PersonType { get; set; }
    public string PersonRole { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateTime Birthday { get; set; }
    public string Gender { get; set; }
    public string Cellphone { get; set; }
    public string Address { get; set; }
}

public class UpdateUserOnlyModel
{
    public int UserId { get; set; }
    public int PersonId { get; set; }
    public int RoleId { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public bool Status { get; set; }
}