using System.ComponentModel.DataAnnotations;

namespace FurryFriends.Domain;

public class PersonType
{
    [Key] public int Id { get; set; }
    public string Name { get; set; }

    public ICollection<Person> Persons { get; set; } = new HashSet<Person>();
}