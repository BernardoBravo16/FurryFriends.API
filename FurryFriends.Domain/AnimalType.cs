using System.ComponentModel.DataAnnotations;

namespace FurryFriends.Domain;

public class AnimalType
{
    [Key] public int Id { get; set; }
    public string Name { get; set; }

    public virtual ICollection<Animal> Animals { get; set; } = new HashSet<Animal>();
    public virtual ICollection<Breed> Breeds { get; set; } = new HashSet<Breed>();
}