namespace FurryFriends.Application.Services.Models;

public class AnimalModel
{
    public int AnimalId { get; set; }
    public int AnimalTypeId { get; set; }
    public string AnimalType { get; set; }
    public int BreedId { get; set; }
    public string Breed { get; set; }
    public int IdentityNumber { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public string AgeType { get; set; }
    public bool Its_Alive { get; set; }
    public bool Is_Adopted { get; set; }
}

public class CreateAnimalModel
{
    public int AnimalTypeId { get; set; }
    public int BreedId { get; set; }
    public int IdentityNumber { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public string AgeType { get; set; }
    public bool Its_Alive { get; set; }
    public bool Is_Adopted { get; set; }
}

public class UpdateAnimalModel
{
    public int AnimalId { get; set; }
    public int IdentityNumber { get; set; }
    public int Age { get; set; }
    public string AgeType { get; set; }
    public bool Its_Alive { get; set; }
    public bool Is_Adopted { get; set; }
}
