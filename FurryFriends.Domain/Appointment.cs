using FurryFriends.Domain.Shared;

namespace FurryFriends.Domain;

public class Appointment : IGenericEntity<int>
{
    public int Id { get; set; }
    public int PersonId { get; set; }
    public int AppointmentReasonId { get; set; }
    public int ShelterId { get; set; }
    public int AppointmentStatusId { get; set; }
    public DateOnly AppointmentDate { get; set; }
    public TimeOnly AppointmentHour { get; set; }

    public virtual Person Person { get; set; }
    public virtual AppointmentReason AppointmentReason { get; set; }
    public virtual Shelter Shelter { get; set; }
    public virtual AppointmentStatus AppointmentStatus { get; set; }
}