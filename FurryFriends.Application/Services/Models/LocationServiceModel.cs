namespace FurryFriends.Application.Services.Models
{
    public class LocationModel
    {
        public int LocationId { get; set; }
        public int AnimalId { get; set; }
        public string Animal { get; set; }
        public int ShelterId { get; set; }
        public string Shelter { get; set; }
        public DateTime Date_From { get; set; }
        public DateTime? Date_To { get; set; }
        public bool Status { get; set; }
    }

    public class CreateLocationModel
    {
        public int AnimalId { get; set; }
        public int ShelterId { get; set; }
        public DateTime Date_From { get; set; }
        public DateTime? Date_To { get; set; }
        public bool Status { get; set; }
    }
}