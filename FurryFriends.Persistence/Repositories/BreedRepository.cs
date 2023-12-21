using FurryFriends.Application.Shared.Contracts.Persistence;
using FurryFriends.Application.Shared.Contracts.Persistence.Repositories;
using FurryFriends.Domain;
using Microsoft.EntityFrameworkCore;

namespace FurryFriends.Persistence.Repositories;

public class BreedRepository : IBreedRepository
{
    private readonly IRepository<Breed, int> _breedRepository;

    public BreedRepository(IRepository<Breed, int> breedRepository)
    {
        _breedRepository = breedRepository;
    }

    public async Task<Breed> GetBreedAsync(int id)
    {
        return await _breedRepository
            .GetAll()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<ICollection<Breed>> GetBreedsAsync()
    {
        return await _breedRepository
            .GetAll()
            .AsNoTracking()
            .ToListAsync();
    }
}