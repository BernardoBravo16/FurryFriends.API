namespace FurryFriends.Application.Services.Models
{
    public class MedicalRecordModel
    {
        public int MedicalRecordId { get; set; }
        public int AnimalId { get; set; }
        public string Animal { get; set; }
        public int VeterinaryId { get; set; }
        public string Veterinary { get; set; }
        public DateTime Treatment_Date { get; set; }
        public string Diagnosis { get; set; }
        public string Treatment { get; set; }
    }

    public class CreateMedicalRecordModel
    {
        public int AnimalId { get; set; }
        public int VeterinaryId { get; set; }
        public DateTime Treatment_Date { get; set; }
        public string Diagnosis { get; set; }
        public string Treatment { get; set; }
    }
}