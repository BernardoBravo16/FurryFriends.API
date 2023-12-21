using FurryFriends.Domain.Shared;
using System.ComponentModel.DataAnnotations;

namespace FurryFriends.Domain;

public class Breed : IGenericEntity<int>
{
    [Key] public int Id { get; set; }
    public int AnimalTypeId { get; set; }
    public string Name { get; set; }

    public virtual AnimalType AnimalType { get; set; }

    public virtual ICollection<Animal> Animals { get; set; } = new HashSet<Animal>();
}