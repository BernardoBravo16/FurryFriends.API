using FurryFriends.Domain;

namespace FurryFriends.Application.Shared.Contracts.Persistence.Repositories;

public interface IContactRepository
{
    Task<Contact> GetContactAsync(int id);
    Task<Contact> GetContactByEmailAsync(string email);
    Task CreateContactAsync(Contact contact);
    Task SaveChangesAsync();
}