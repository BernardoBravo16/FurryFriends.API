using FurryFriends.Application.Shared.Contracts.Persistence;
using FurryFriends.Application.Shared.Contracts.Persistence.Repositories;
using FurryFriends.Domain;
using Microsoft.EntityFrameworkCore;

namespace FurryFriends.Persistence.Repositories;

public class ContactRepository : IContactRepository
{
    private readonly IRepository<Contact, int> _contactRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ContactRepository(IRepository<Contact, int> contactRepository, IUnitOfWork unitOfWork)
    {
        _contactRepository = contactRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Contact> GetContactAsync(int id)
    {
        return await _contactRepository
            .GetAll()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Contact> GetContactByEmailAsync(string email)
    {
        return await _contactRepository
            .GetAll()
            .FirstOrDefaultAsync(x => x.Email == email);
    }

    public async Task CreateContactAsync(Contact contact)
    {
        await _contactRepository.AddAsync(contact);

        await SaveChangesAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _unitOfWork.SaveAsync();
    }
}
