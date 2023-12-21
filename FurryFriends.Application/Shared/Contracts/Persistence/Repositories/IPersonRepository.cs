using FurryFriends.Domain;

namespace FurryFriends.Application.Shared.Contracts.Persistence.Repositories;

public interface IPersonRepository
{
    Task<Person> GetPersonAsync(int id);
    Task<Person> GetPersonByIdentityCardAsync(string identityCard);
    Task<ICollection<Person>> GetPersonsAsync();
    Task<ICollection<Person>> GetPersonsByPersonRoleAsync(int personRoleId);
    Task CreatePersonAsync(Person person);
    Task SaveChangesAsync();
}