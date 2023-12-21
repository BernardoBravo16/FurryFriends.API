using FurryFriends.Domain;

namespace FurryFriends.Application.Shared.Contracts.Persistence.Repositories;

public interface IBreedRepository
{
    Task<Breed> GetBreedAsync(int id);
    Task<ICollection<Breed>> GetBreedsAsync();
}