using FurryFriends.Application.Shared.Contracts.Persistence;
using FurryFriends.Application.Shared.Contracts.Persistence.Repositories;
using FurryFriends.Domain;
using Microsoft.EntityFrameworkCore;

namespace FurryFriends.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IRepository<User, int> _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UserRepository(IRepository<User, int> userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<User> GetUserAsync(int id)
    {
        return await _userRepository
            .GetAll()
            .Include(x => x.Person)
            .Include(x => x.Person.PersonType)
            .Include(x => x.Person.PersonRole)
            .Include(x => x.Person.Contact)
            .Include(x => x.Role)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<ICollection<User>> GetUsersAsync()
    {
        return await _userRepository
            .GetAll()
            .Include(x => x.Person)
            .Include(x => x.Person.PersonType)
            .Include(x => x.Person.PersonRole)
            .Include(x => x.Person.Contact)
            .Include(x => x.Role)
            .ToListAsync();
    }

    public async Task<User> GetUserByUsernameAsync(string username)
    {
        return await _userRepository
            .GetAll()
            .Include(x => x.Person)
            .Include(x => x.Person.PersonType)
            .Include(x => x.Person.Contact)
            .Include(x => x.Person.PersonRole)
            .Include(x => x.Role)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Username == username);
    }

    public async Task<User> GetUserByPersonIdAsync(int personId)
    {
        return await _userRepository
            .GetAll()
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Person.Id == personId);
    }

    public async Task CreateUserAsync(User user)
    {
        await _userRepository.AddAsync(user);

        await SaveChangesAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _unitOfWork.SaveAsync();
    }
}
