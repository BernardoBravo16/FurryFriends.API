using FurryFriends.Application.Shared.Contracts.Persistence;
using FurryFriends.Application.Shared.Contracts.Persistence.Repositories;
using FurryFriends.Domain;
using Microsoft.EntityFrameworkCore;

namespace FurryFriends.Persistence.Repositories;

public class PersonRepository : IPersonRepository
{
    private readonly IRepository<Person, int> _personRepository;
    private readonly IUnitOfWork _unitOfWork;

    public PersonRepository(IRepository<Person, int> personRepository, IUnitOfWork unitOfWork)
    {
        _personRepository = personRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Person> GetPersonAsync(int id)
    {
        return await _personRepository
            .GetAll()
            .Include(x => x.PersonType)
            .Include(x => x.PersonRole)
            .Include(x => x.Contact)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Person> GetPersonByIdentityCardAsync(string identityCard)
    {
        return await _personRepository
            .GetAll()
            .Include(x => x.PersonType)
            .Include(x => x.PersonRole)
            .Include(x => x.Contact)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.IdentityCard == identityCard);
    }

    public async Task<ICollection<Person>> GetPersonsAsync()
    {
        return await _personRepository
            .GetAll()
            .Include(x => x.PersonType)
            .Include(x => x.PersonRole)
            .Include(x => x.Contact)
            .ToListAsync();
    }

    public async Task<ICollection<Person>> GetPersonsByPersonRoleAsync(int personRoleId)
    {
        return await _personRepository
            .GetAll()
            .Include(x => x.PersonType)
            .Include(x => x.PersonRole)
            .Include(x => x.Contact)
            .AsNoTracking()
            .Where(x => x.PersonRoleId == personRoleId)
            .ToListAsync();
    }

    public async Task CreatePersonAsync(Person person)
    {
        await _personRepository.AddAsync(person);

        await SaveChangesAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _unitOfWork.SaveAsync();
    }
}
