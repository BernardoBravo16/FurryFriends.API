using FurryFriends.Domain.Shared;
using System.ComponentModel.DataAnnotations;

namespace FurryFriends.Domain;

public class Shelter : IGenericEntity<int>
{
    [Key] public int Id { get; set; }
    public int ContactId { get; set; }
	public string Name { get; set; }
	public int Capacity { get; set; }

    public virtual Contact Contact { get; set; }

    public ICollection<Appointment> Appointments { get; set; } = new HashSet<Appointment>();
	public virtual ICollection<Location> Locations { get; set; } = new HashSet<Location>();
}