using FurryFriends.Domain.Shared;
using System.ComponentModel.DataAnnotations;

namespace FurryFriends.Domain;

public class Location : IGenericEntity<int>
{
    [Key] public int Id { get; set; }
    public int AnimalId { get; set; }
	public int ShelterId { get; set; }
	public DateTime Date_From { get; set; }
	public DateTime? Date_To { get; set; }
	public bool Status { get; set; }

	public virtual Animal Animal { get; set; }
	public virtual Shelter Shelter { get; set; }
}