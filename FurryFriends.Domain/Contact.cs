using FurryFriends.Domain.Shared;
using System.ComponentModel.DataAnnotations;

namespace FurryFriends.Domain;

public class Contact : IGenericEntity<int>
{
    [Key] public int Id { get; set; }
    public string Cellphone { get; set; }
	public string Email { get; set; }
	public string Address { get; set; }

    public ICollection<Person> Persons { get; set; } = new HashSet<Person>();
    public ICollection<Shelter> Shelters { get; set; } = new HashSet<Shelter>();
}