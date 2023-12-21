using FurryFriends.Application.Services.Interfaces;
using FurryFriends.Application.Services.Models;
using FurryFriends.Application.Shared;
using FurryFriends.Application.Shared.Contracts.Persistence.Repositories;
using FurryFriends.Application.Shared.Models.Base;
using FurryFriends.Application.Shared.Resources;
using FurryFriends.Domain.Enum;
using FurryFriends.Domain;
using System.Net;

namespace FurryFriends.Application.Services;

public class PersonService : BaseService, IPersonService
{

    private readonly IPersonRepository _personRepository;
    private readonly IContactRepository _contactRepository;
    private readonly IUserRepository _userRepository;

    public PersonService(IPersonRepository personRepository,
        IContactRepository contactRepository,
        IUserRepository userRepository)
    {
        _personRepository = personRepository;
        _contactRepository = contactRepository;
        _userRepository = userRepository;
    }

    public async Task<ServiceResponse> GetPersonAsync(int id)
    {
        var serviceResponse = new ServiceResponse();

        try
        {
            var personDb = await _personRepository.GetPersonAsync(id);

            if (personDb == null)
            {
                serviceResponse.SetFaultyState(HttpStatusCode.NotFound, Resource.PersonNotFoundMessage);
                return serviceResponse;
            }

            var person = new PersonModel
            {
                PersonId = personDb.Id,
                PersonTypeId = personDb.PersonTypeId,
                PersonType = personDb.PersonType.Name,
                PersonRoleId = personDb.PersonRoleId,
                PersonRole = personDb.PersonRole.Name,
                IdentityCard = personDb.IdentityCard,
                Name = personDb.Name,
                Surname = personDb.Surname,
                Birthday = personDb.Birthday,
                Gender = personDb.Gender,
                Status = personDb.Status,
                Email = personDb.Contact.Email,
                Cellphone = personDb.Contact.Cellphone,
                Address = personDb.Contact.Address
            };

            serviceResponse.Data = person;

            return serviceResponse;
        }
        catch (Exception ex)
        {
            serviceResponse.SetFaultyState(HttpStatusCode.InternalServerError, ex);
            return serviceResponse;
        }
    }

    public async Task<ServiceResponse> GetPersonsAsync()
    {
        var serviceResponse = new ServiceResponse();

        try
        {

            var personsDb = await _personRepository.GetPersonsAsync();

            if (personsDb == null)
            {
                serviceResponse.SetFaultyState(HttpStatusCode.NotFound, Resource.PersonNotFoundMessage);
                return serviceResponse;
            }

            var persons = personsDb.Select(x => new PersonModel
            {
                PersonId = x.Id,
                PersonTypeId = x.PersonTypeId,
                PersonType = x.PersonType.Name,
                PersonRoleId = x.PersonRoleId,
                PersonRole = x.PersonRole.Name,
                IdentityCard = x.IdentityCard,
                Name = x.Name,
                Surname = x.Surname,
                Birthday = x.Birthday,
                Gender = x.Gender,
                Status = x.Status,
                Email = x.Contact.Email,
                Cellphone = x.Contact.Cellphone,
                Address = x.Contact.Address

            });

            serviceResponse.Data = persons;

            return serviceResponse;
        }
        catch (Exception ex)
        {
            serviceResponse.SetFaultyState(HttpStatusCode.InternalServerError, ex);
            return serviceResponse;
        }
    }

    public async Task<ServiceResponse> GetPersonsNotUsersAsync()
    {
        var serviceResponse = new ServiceResponse();

        var personsDb = await _personRepository.GetPersonsAsync();

        if (personsDb == null)
        {
            serviceResponse.SetFaultyState(HttpStatusCode.NotFound, Resource.PersonNotFoundMessage);
            return serviceResponse;
        }

        var usersDb = await _userRepository.GetUsersAsync();

        if (usersDb == null)
        {
            serviceResponse.SetFaultyState(HttpStatusCode.NotFound, Resource.UsersNotFoundMessage);
            return serviceResponse;
        }

        var personsWithoutUsers = personsDb
            .Where(person => !usersDb.Any(user => user.PersonId == person.Id))
            .ToList();

        var persons = personsWithoutUsers.Select(x => new PersonModel
        {
            PersonId = x.Id,
            PersonTypeId = x.PersonTypeId,
            PersonType = x.PersonType.Name,
            PersonRoleId = x.PersonRoleId,
            PersonRole = x.PersonRole.Name,
            IdentityCard = x.IdentityCard,
            Name = x.Name,
            Surname = x.Surname,
            Birthday = x.Birthday,
            Gender = x.Gender,
            Status = x.Status,
            Email = x.Contact.Email,
            Cellphone = x.Contact.Cellphone,
            Address = x.Contact.Address
        });

        serviceResponse.Data = persons;

        return serviceResponse;

    }

    public async Task<ServiceResponse> GetPersonsByPersonRoleAsync(string role)
    {
        var serviceResponse = new ServiceResponse();

        try
        {
            var personRole = (PersonRoleEnum)Enum.Parse(typeof(PersonRoleEnum), role);

            var personsDb = await _personRepository.GetPersonsByPersonRoleAsync((int)personRole);

            if (personsDb == null)
            {
                serviceResponse.SetFaultyState(HttpStatusCode.NotFound, Resource.PersonNotFoundMessage);
                return serviceResponse;
            }

            var persons = personsDb.Select(x => new PersonModel
            {
                PersonId = x.Id,
                PersonTypeId = x.PersonTypeId,
                PersonType = x.PersonType.Name,
                PersonRoleId = x.PersonRoleId,
                PersonRole = x.PersonRole.Name,
                IdentityCard = x.IdentityCard,
                Name = x.Name,
                Surname = x.Surname,
                Birthday = x.Birthday,
                Gender = x.Gender,
                Status = x.Status,
                Email = x.Contact.Email,
                Cellphone = x.Contact.Cellphone,
                Address = x.Contact.Address

            });

            serviceResponse.Data = persons;

            return serviceResponse;
        }
        catch (Exception ex)
        {
            serviceResponse.SetFaultyState(HttpStatusCode.InternalServerError, ex);
            return serviceResponse;
        }
    }

    public async Task<ServiceResponse> CreatePersonAsync(CreatePersonModel model)
    {
        var serviceResponse = new ServiceResponse();

        try
        {
            var personDb = await _personRepository.GetPersonByIdentityCardAsync(model.IdentityCard);

            if (personDb != null)
            {
                serviceResponse.SetFaultyState(HttpStatusCode.BadRequest, Resource.PersonAlreadyExistsMessage);
                return serviceResponse;
            }

            var contactDb = await _contactRepository.GetContactByEmailAsync(model.Email);

            if (contactDb != null)
            {
                serviceResponse.SetFaultyState(HttpStatusCode.BadRequest, Resource.PersonEmailAlreadyExistsMessage);
                return serviceResponse;
            }

            var personType = (PersonTypeEnum)Enum.Parse(typeof(PersonTypeEnum), model.PersonType);
            var personRole = (PersonRoleEnum)Enum.Parse(typeof(PersonRoleEnum), model.PersonRole);

            var person = new Person
            {
                PersonTypeId = (int)personType,
                PersonRoleId = (int)personRole,
                ContactId = 0,
                IdentityCard = model.IdentityCard,
                Name = model.Name,
                Surname = model.Surname,
                Birthday = model.Birthday,
                Gender = model.Gender,
                Status = true,
                Contact = new Contact
                {
                    Cellphone = model.Cellphone,
                    Email = model.Email,
                    Address = model.Address
                }
            };

            await _personRepository.CreatePersonAsync(person);

            var personCreated = new PersonModel
            {
                PersonId = person.Id,
                PersonTypeId = person.PersonTypeId,
                PersonType = personType.ToString(),
                PersonRoleId = person.PersonRoleId,
                PersonRole = personRole.ToString(),
                IdentityCard = person.IdentityCard,
                Name = person.Name,
                Surname = person.Surname,
                Birthday = person.Birthday,
                Gender = person.Gender,
                Status = person.Status,
                Email = person.Contact.Email,
                Cellphone = person.Contact.Cellphone,
                Address = person.Contact.Address
            };

            serviceResponse.Data = personCreated;

            return serviceResponse;
        }
        catch (Exception ex)
        {
            serviceResponse.SetFaultyState(HttpStatusCode.InternalServerError, ex);
            return serviceResponse;
        }
    }

    public async Task<ServiceResponse> UpdatePersonAsync(UpdatePersonModel model)
    {
        var serviceResponse = new ServiceResponse();

        try
        {
            var personDb = await _personRepository.GetPersonAsync(model.PersonId);

            if (personDb == null)
            {
                serviceResponse.SetFaultyState(HttpStatusCode.NotFound, Resource.PersonNotFoundMessage);
                return serviceResponse;
            }

            var personType = (PersonTypeEnum)Enum.Parse(typeof(PersonTypeEnum), model.PersonType);
            var personRole = (PersonRoleEnum)Enum.Parse(typeof(PersonRoleEnum), model.PersonRole);

            personDb.PersonTypeId = (int)personType;
            personDb.PersonRoleId = (int)personRole;
            personDb.Name = model.Name;
            personDb.Surname = model.Surname;
            personDb.Birthday = model.Birthday;
            personDb.Gender = model.Gender;
            personDb.Status = model.Status;
            personDb.Contact.Cellphone = model.Cellphone;
            personDb.Contact.Address = model.Address;

            await _personRepository.SaveChangesAsync();

            var person = new PersonModel
            {
                PersonId = personDb.Id,
                PersonTypeId = personDb.PersonTypeId,
                PersonType = personType.ToString(),
                PersonRoleId = personDb.PersonRoleId,
                PersonRole = personRole.ToString(),
                IdentityCard = personDb.IdentityCard,
                Name = personDb.Name,
                Surname = personDb.Surname,
                Birthday = personDb.Birthday,
                Gender = personDb.Gender,
                Status = personDb.Status,
                Email = personDb.Contact.Email,
                Cellphone = personDb.Contact.Cellphone,
                Address = personDb.Contact.Address
            };

            serviceResponse.Data = person;

            return serviceResponse;
        }
        catch (Exception ex)
        {
            serviceResponse.SetFaultyState(HttpStatusCode.InternalServerError, ex);
            return serviceResponse;
        }
    }
}
