using FurryFriends.Application.Shared.Contracts.Persistence;
using FurryFriends.Application.Shared.Contracts.Persistence.Repositories;
using FurryFriends.Domain;
using Microsoft.EntityFrameworkCore;

namespace FurryFriends.Persistence.Repositories;

public class ShelterRepository : IShelterRepository
{
    private readonly IRepository<Shelter, int> _shelterRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ShelterRepository(IRepository<Shelter, int> shelterRepository, IUnitOfWork unitOfWork)
    {
        _shelterRepository = shelterRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Shelter> GetShelterAsync(int id)
    {
        return await _shelterRepository
            .GetAll()
            .Include(x => x.Contact)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<ICollection<Shelter>> GetSheltersAsync()
    {
        return await _shelterRepository
            .GetAll()
            .Include(x => x.Contact)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Shelter> GetShelterByNameAsync(string shelterName)
    {
        return await _shelterRepository
            .GetAll()
            .Include(x => x.Contact)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Name == shelterName);
    }

    public async Task CreateShelterAsync(Shelter shelter)
    {
        await _shelterRepository.AddAsync(shelter);

        await SaveChangesAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _unitOfWork.SaveAsync();
    }
}