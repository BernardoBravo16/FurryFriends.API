namespace FurryFriends.Domain;

public class AppointmentStatus
{
    public int Id { get; set; }
    public string Name { get; set; }

    public ICollection<Appointment> Appointments { get; set; } = new HashSet<Appointment>();
}
