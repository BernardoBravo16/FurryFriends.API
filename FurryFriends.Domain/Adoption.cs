using FurryFriends.Domain.Shared;
using System.ComponentModel.DataAnnotations;

namespace FurryFriends.Domain;

public class Adoption : IGenericEntity<int>
{
	[Key] public int Id { get; set; }
	public int AnimalId { get; set; }
	public int PersonId { get; set; }
	public DateTime Adoption_Date { get; set; }

	public virtual Animal Animal { get; set; }
	public virtual Person Person { get; set; }
}