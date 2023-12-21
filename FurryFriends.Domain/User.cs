using FurryFriends.Domain.Shared;
using System.ComponentModel.DataAnnotations;

namespace FurryFriends.Domain;

public class User : IGenericEntity<int>
{
    public int Id { get; set; }
    public int PersonId { get; set; }
	public int RoleId { get; set; }
	public string Username { get; set; }
	public string Password { get; set; }
	public bool Status { get; set; }

	public virtual Person Person { get; set; }
	public virtual Role Role { get; set; }
}