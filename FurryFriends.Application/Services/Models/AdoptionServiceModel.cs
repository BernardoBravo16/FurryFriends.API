namespace FurryFriends.Application.Services.Models
{
    public class AdoptionModel
    {
        public int AdoptionId { get; set; }
        public int AnimalId { get; set; }
        public string Animal { get; set; }
        public int PersonId { get; set; }
        public string Person { get; set; }
        public DateTime Adoption_Date { get; set; }
    }

    public class CreateAdoptionModel
    {
        public int AnimalId { get; set; }
        public int PersonId { get; set; }
        public DateTime Adoption_Date { get; set; }
    }
}