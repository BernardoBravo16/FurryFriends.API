using FurryFriends.Domain;

namespace FurryFriends.Application.Shared.Contracts.Persistence.Repositories;

public interface IAnimalRepository
{
    Task<Animal> GetAnimalAsync(int id);
    Task<ICollection<Animal>> GetAnimalsAsync();
    Task<Animal> GetAnimalByIdentityNumberAsync(int identityNumber);
    Task CreateAnimalAsync(Animal animal);
    Task SaveChangesAsync();
}