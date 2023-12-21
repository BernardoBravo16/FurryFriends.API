using FurryFriends.Domain.Shared;
using System.ComponentModel.DataAnnotations;

namespace FurryFriends.Domain;

public class Animal : IGenericEntity<int>
{
    [Key] public int Id { get; set; }
    public int AnimalTypeId { get; set; }
	public int BreedId { get; set; }
	public int IdentityNumber { get; set; }
	public string Name { get; set; }
    public int Age { get; set; }
	public string AgeType { get; set; }
	public bool Its_Alive { get; set; }
	public bool Is_Adopted { get; set; }

    public virtual AnimalType AnimalType { get; set; }
    public virtual Breed Breed { get; set; }

	public virtual ICollection<Location> Locations { get; set; } = new HashSet<Location>();
	public virtual ICollection<MedicalRecord> MedicalRecords { get; set; } = new HashSet<MedicalRecord>();
	public virtual ICollection<Adoption> Adoptions { get; set; } = new HashSet<Adoption>();
}