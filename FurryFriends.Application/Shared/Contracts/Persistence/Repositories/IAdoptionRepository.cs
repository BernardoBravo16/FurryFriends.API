using FurryFriends.Domain;

namespace FurryFriends.Application.Shared.Contracts.Persistence.Repositories;

public interface IAdoptionRepository
{
    Task<Adoption> GetAdoptionAsync(int id);
    Task<ICollection<Adoption>> GetAdoptionsByAnimalIdAsync(int animalId);
    Task<ICollection<Adoption>> GetAdoptionsByPersonIdAsync(int personId);
    Task CreateAdoptionAsync(Adoption adoption);
    Task SaveChangesAsync();
}