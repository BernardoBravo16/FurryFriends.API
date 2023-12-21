namespace FurryFriends.Application.Services.Models;

public class PersonModel
{
    public int PersonId { get; set; }
    public int PersonTypeId { get; set; }
    public string PersonType { get; set; }
    public int PersonRoleId { get; set; }
    public string PersonRole { get; set; }
    public string IdentityCard { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string FullName => $"{Name} {Surname}";
    public DateTime Birthday { get; set; }
    public string Gender { get; set; }
    public bool Status { get; set; }
    public string Email { get; set; }
    public string Cellphone { get; set; }
    public string Address { get; set; }
}

public class CreatePersonModel
{
    public string PersonType { get; set; }
    public string PersonRole { get; set; }
    public string IdentityCard { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateTime Birthday { get; set; }
    public string Gender { get; set; }
    public string Email { get; set; }
    public string Cellphone { get; set; }
    public string Address { get; set; }
}

public class UpdatePersonModel
{
    public int PersonId { get; set; }
    public string PersonType { get; set; }
    public string PersonRole { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateTime Birthday { get; set; }
    public string Gender { get; set; }
    public bool Status { get; set; }
    public string Cellphone { get; set; }
    public string Address { get; set; }
}