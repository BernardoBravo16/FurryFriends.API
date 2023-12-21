using FurryFriends.Domain;

namespace FurryFriends.Application.Shared.Contracts.Persistence.Repositories;

public interface IUserRepository
{
    Task<User> GetUserAsync(int id);
    Task<ICollection<User>> GetUsersAsync();
    Task<User> GetUserByUsernameAsync(string username);
    Task<User> GetUserByPersonIdAsync(int personId);
    Task CreateUserAsync(User user);
    Task SaveChangesAsync();
}