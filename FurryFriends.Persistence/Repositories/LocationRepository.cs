using FurryFriends.Application.Shared.Contracts.Persistence;
using FurryFriends.Application.Shared.Contracts.Persistence.Repositories;
using FurryFriends.Domain;
using Microsoft.EntityFrameworkCore;

namespace FurryFriends.Persistence.Repositories;

public class LocationRepository : ILocationRepository
{
    private readonly IRepository<Location, int> _locationRepository;
    private readonly IUnitOfWork _unitOfWork;

    public LocationRepository(IRepository<Location, int> locationRepository, IUnitOfWork unitOfWork)
    {
        _locationRepository = locationRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Location> GetLocationAsync(int id)
    {
        return await _locationRepository
            .GetAll()
            .Include(x => x.Animal)
            .Include(x => x.Shelter)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<ICollection<Location>> GetLocationsByAnimalIdAsync(int animalId)
    {
        return await _locationRepository
            .GetAll()
            .Include(x => x.Animal)
            .Include(x => x.Shelter)
            .Where(x => x.AnimalId == animalId)
            .OrderByDescending(x => x.Date_From)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<ICollection<Location>> GetLocationsByShelterIdAsync(int shelterId)
    {
        return await _locationRepository
            .GetAll()
            .Include(x => x.Animal)
            .Include(x => x.Shelter)
            .Where(x => x.ShelterId == shelterId)
            .OrderByDescending(x => x.Date_From)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task CreateLocationAsync(Location location)
    {
        await _locationRepository.AddAsync(location);

        await SaveChangesAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _unitOfWork.SaveAsync();
    }
}
