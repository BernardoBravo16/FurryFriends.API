using FurryFriends.Application.Shared.Contracts.Persistence;
using FurryFriends.Application.Shared.Contracts.Persistence.Repositories;
using FurryFriends.Domain;
using Microsoft.EntityFrameworkCore;

namespace FurryFriends.Persistence.Repositories;

public class AdoptionRepository : IAdoptionRepository
{
    private readonly IRepository<Adoption, int> _adoptionRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AdoptionRepository(IRepository<Adoption, int> adoptionRepository, IUnitOfWork unitOfWork)
    {
        _adoptionRepository = adoptionRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Adoption> GetAdoptionAsync(int id)
    {
        return await _adoptionRepository
            .GetAll()
            .Include(x => x.Animal)
            .Include(x => x.Person)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<ICollection<Adoption>> GetAdoptionsByAnimalIdAsync(int animalId)
    {
        return await _adoptionRepository
            .GetAll()
            .Include(x => x.Animal)
            .Include(x => x.Person)
            .Where(x => x.AnimalId == animalId)
            .OrderByDescending(x => x.Adoption_Date)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<ICollection<Adoption>> GetAdoptionsByPersonIdAsync(int personId)
    {
        return await _adoptionRepository
            .GetAll()
            .Include(x => x.Animal)
            .Include(x => x.Person)
            .Where(x => x.PersonId == personId)
            .OrderByDescending(x => x.Adoption_Date)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task CreateAdoptionAsync(Adoption adoption)
    {
        await _adoptionRepository.AddAsync(adoption);

        await SaveChangesAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _unitOfWork.SaveAsync();
    }
}
