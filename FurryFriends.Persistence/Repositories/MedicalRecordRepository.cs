using FurryFriends.Application.Shared.Contracts.Persistence;
using FurryFriends.Application.Shared.Contracts.Persistence.Repositories;
using FurryFriends.Domain;
using Microsoft.EntityFrameworkCore;

namespace FurryFriends.Persistence.Repositories;

public class MedicalRecordRepository : IMedicalRecordRepository
{
    private readonly IRepository<MedicalRecord, int> _medicalRecordRepository;
    private readonly IUnitOfWork _unitOfWork;

    public MedicalRecordRepository(IRepository<MedicalRecord, int> medicalRecordRepository, IUnitOfWork unitOfWork)
    {
        _medicalRecordRepository = medicalRecordRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<MedicalRecord> GetMedicalRecordAsync(int id)
    {
        return await _medicalRecordRepository
            .GetAll()
            .Include(x => x.Animal)
            .Include(x => x.Person)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<ICollection<MedicalRecord>> GetMedicalRecordsByAnimalIdAsync(int animalId)
    {
        return await _medicalRecordRepository
            .GetAll()
            .Include(x => x.Animal)
            .Include(x => x.Person)
            .Where(x => x.AnimalId == animalId)
            .ToListAsync();
    }

    public async Task<ICollection<MedicalRecord>> GetMedicalRecordsByPersonIdAsync(int personId)
    {
        return await _medicalRecordRepository
            .GetAll()
            .Include(x => x.Animal)
            .Include(x => x.Person)
            .Where(x => x.VeterinaryId == personId)
            .ToListAsync();
    }

    public async Task CreateMedicalRecordAsync(MedicalRecord medicalRecord)
    {
        await _medicalRecordRepository.AddAsync(medicalRecord);

        await SaveChangesAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _unitOfWork.SaveAsync();
    }
}
