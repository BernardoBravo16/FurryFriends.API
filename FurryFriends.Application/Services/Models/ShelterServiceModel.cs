namespace FurryFriends.Application.Services.Models;

public class ShelterModel
{
    public int ShelterId { get; set; }
    public string ShelterName { get; set; }
    public int Capacity { get; set; }
    public int ContactId { get; set; }
    public string Cellphone { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
}

public class CreateShelterModel
{
    public string ShelterName { get; set; }
    public int Capacity { get; set; }
    public int ContactId { get; set; }
    public string Cellphone { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
}

public class UpdateShelterModel
{
    public int ShelterId { get; set; }
    public string ShelterName { get; set; }
    public int Capacity { get; set; }
    public string Cellphone { get; set; }
    public string Address { get; set; }
}