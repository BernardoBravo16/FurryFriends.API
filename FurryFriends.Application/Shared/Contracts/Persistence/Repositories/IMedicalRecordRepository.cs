using FurryFriends.Domain;

namespace FurryFriends.Application.Shared.Contracts.Persistence.Repositories;

public interface IMedicalRecordRepository
{
    Task<MedicalRecord> GetMedicalRecordAsync(int id);
    Task<ICollection<MedicalRecord>> GetMedicalRecordsByAnimalIdAsync(int animalId);
    Task<ICollection<MedicalRecord>> GetMedicalRecordsByPersonIdAsync(int personId);
    Task CreateMedicalRecordAsync(MedicalRecord medicalRecord);
    Task SaveChangesAsync();
}