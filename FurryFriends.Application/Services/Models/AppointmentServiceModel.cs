namespace FurryFriends.Application.Services.Models
{
    public class AppointmentModel
    {
        public int AppointmentId { get; set; }
        public int PersonId { get; set; }
        public string Person { get; set; }
        public int AppointmentReasonId { get; set; }
        public string AppointmentReason { get; set; }
        public int ShelterId { get; set; }
        public string Shelter { get; set; }
        public int AppointmentStatusId { get; set; }
        public string AppointmentStatus { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly Hour { get; set; }
    }

    public class CreateAppointmentModel
    {
        public int PersonId { get; set; }
        public int AppointmentReasonId { get; set; }
        public int ShelterId { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly Hour { get; set; }
    }

    public class UpdateAppointmentModel
    {
        public int AppointmentId { get; set; }
        public int AppointmentReasonId { get; set; }
        public int ShelterId { get; set; }
        public int AppointmentStatusId { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly Hour { get; set; }
    }

    public class AppointmentReasonModel
    {
        public int AppointmentReasonId { get; set; }
        public string Name { get; set; }
    }
}