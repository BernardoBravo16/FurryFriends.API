using FurryFriends.Application.Shared.Contracts.Persistence;
using FurryFriends.Application.Shared.Contracts.Persistence.Repositories;
using FurryFriends.Domain;
using Microsoft.EntityFrameworkCore;

namespace FurryFriends.Persistence.Repositories;

public class AnimalRepository : IAnimalRepository
{
    private readonly IRepository<Animal, int> _animalRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AnimalRepository(IRepository<Animal, int> animalRepository, IUnitOfWork unitOfWork)
    {
        _animalRepository = animalRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Animal> GetAnimalAsync(int id)
    {
        return await _animalRepository
            .GetAll()
            .Include(x => x.AnimalType)
            .Include(x => x.Breed)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<ICollection<Animal>> GetAnimalsAsync()
    {
        return await _animalRepository
            .GetAll()
            .Include(x => x.AnimalType)
            .Include(x => x.Breed)
            .ToListAsync();
    }

    public async Task<Animal> GetAnimalByIdentityNumberAsync(int identityNumber)
    {
        return await _animalRepository
            .GetAll()
            .Include(x => x.AnimalType)
            .Include(x => x.Breed)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.IdentityNumber == identityNumber);
    }

    public async Task CreateAnimalAsync(Animal animal)
    {
        await _animalRepository.AddAsync(animal);

        await SaveChangesAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _unitOfWork.SaveAsync();
    }
}
