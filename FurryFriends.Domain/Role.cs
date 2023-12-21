using System.ComponentModel.DataAnnotations;

namespace FurryFriends.Domain;

public class Role
{
    [Key] public int Id { get; set; }
    public string Name { get; set; }
	public bool Status { get; set; }

	public ICollection<User> Users {  get; set; } = new HashSet<User>();
}