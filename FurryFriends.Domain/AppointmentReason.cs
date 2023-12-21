using FurryFriends.Domain.Shared;

namespace FurryFriends.Domain;

public class AppointmentReason : IGenericEntity<int>
{
    public int Id { get; set; }
    public string Name { get; set; }

    public ICollection<Appointment> Appointments { get; set; } = new HashSet<Appointment>();
}