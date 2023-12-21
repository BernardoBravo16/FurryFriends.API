using FurryFriends.Domain;

namespace FurryFriends.Application.Shared.Contracts.Persistence.Repositories;

public interface IShelterRepository
{
    Task<Shelter> GetShelterAsync(int id);
    Task<ICollection<Shelter>> GetSheltersAsync();
    Task<Shelter> GetShelterByNameAsync(string shelterName);
    Task CreateShelterAsync(Shelter shelter);
    Task SaveChangesAsync();
}