using FurryFriends.Domain;

namespace FurryFriends.Application.Shared.Contracts.Persistence.Repositories;

public interface ILocationRepository
{
    Task<Location> GetLocationAsync(int id);
    Task<ICollection<Location>> GetLocationsByAnimalIdAsync(int animalId);
    Task<ICollection<Location>> GetLocationsByShelterIdAsync(int shelterId);
    Task CreateLocationAsync(Location location);
    Task SaveChangesAsync();
}