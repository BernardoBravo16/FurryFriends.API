using FurryFriends.Application.Services.Authentication.Interfaces;
using FurryFriends.Application.Services.Interfaces;
using FurryFriends.Application.Services.Models;
using FurryFriends.Application.Shared;
using FurryFriends.Application.Shared.Contracts.Persistence.Repositories;
using FurryFriends.Application.Shared.Models.Base;
using FurryFriends.Application.Shared.Resources;
using FurryFriends.Domain;
using System.Net;

namespace FurryFriends.Application.Services;

public class ShelterService : BaseService, IShelterService
{

    private readonly IShelterRepository _shelterRepository;
    private readonly IContactRepository _contactRepository;

    public ShelterService(IShelterRepository shelterRepository,
        IContactRepository contactRepository)
    {
        _shelterRepository = shelterRepository;
        _contactRepository = contactRepository;
    }

    public async Task<ServiceResponse> GetShelterAsync(int id)
    {
        var serviceResponse = new ServiceResponse();

        try
        {
            var shelterDb = await _shelterRepository.GetShelterAsync(id);

            if (shelterDb == null)
            {
                serviceResponse.SetFaultyState(HttpStatusCode.NotFound, Resource.ShelterNotFoundMessage);
                return serviceResponse;
            }

            var shelter = new ShelterModel
            {
                ShelterId = shelterDb.Id,
                ShelterName = shelterDb.Name,
                ContactId = shelterDb.ContactId,
                Cellphone = shelterDb.Contact.Cellphone,
                Email = shelterDb.Contact.Email,
                Address = shelterDb.Contact.Address,
                Capacity = shelterDb.Capacity
            };

            serviceResponse.Data = shelter;

            return serviceResponse;
        }
        catch (Exception ex)
        {
            serviceResponse.SetFaultyState(HttpStatusCode.InternalServerError, ex);
            return serviceResponse;
        }
    }

    public async Task<ServiceResponse> GetSheltersAsync()
    {
        var serviceResponse = new ServiceResponse();

        try
        {
            var sheltersDb = await _shelterRepository.GetSheltersAsync();

            if (sheltersDb == null)
            {
                serviceResponse.SetFaultyState(HttpStatusCode.NotFound, Resource.SheltersNotFoundMessage);
                return serviceResponse;
            }

            var shelters = sheltersDb.Select(x => new ShelterModel
            {
                ShelterId = x.Id,
                ShelterName = x.Name,
                ContactId = x.ContactId,
                Cellphone = x.Contact.Cellphone,
                Email = x.Contact.Email,
                Address = x.Contact.Address,
                Capacity = x.Capacity
            });

            serviceResponse.Data = shelters;

            return serviceResponse;
        }
        catch (Exception ex)
        {
            serviceResponse.SetFaultyState(HttpStatusCode.InternalServerError, ex);
            return serviceResponse;
        }
    }

    public async Task<ServiceResponse> CreateShelterAsync(CreateShelterModel model)
    {
        var serviceResponse = new ServiceResponse();

        try
        {
            var shelterDb = await _shelterRepository.GetShelterByNameAsync(model.ShelterName);

            if (shelterDb != null)
            {
                serviceResponse.SetFaultyState(HttpStatusCode.BadRequest, Resource.ShelterAlreadyExistsMessage);
                return serviceResponse;
            }

            var contactDb = await _contactRepository.GetContactByEmailAsync(model.Email);

            if (contactDb != null)
            {
                serviceResponse.SetFaultyState(HttpStatusCode.BadRequest, Resource.ShelterEmailAlreadyExistsMessage);
                return serviceResponse;
            }

            var shelter = new Shelter
            {
                Name = model.ShelterName,
                Capacity = model.Capacity,
                ContactId = 0,
                Contact = new Contact
                {
                    Cellphone = model.Cellphone,
                    Email = model.Email,
                    Address = model.Address
                }
            };

            await _shelterRepository.CreateShelterAsync(shelter);

            var shelterCreated = new ShelterModel
            {
                ShelterId = shelter.Id,
                ShelterName = shelter.Name,
                ContactId = shelter.ContactId,
                Cellphone = shelter.Contact.Cellphone,
                Email = shelter.Contact.Email,
                Address = shelter.Contact.Address,
                Capacity = shelter.Capacity
            };

            serviceResponse.Data = shelterCreated;

            return serviceResponse;
        }
        catch (Exception ex)
        {
            serviceResponse.SetFaultyState(HttpStatusCode.InternalServerError, ex);
            return serviceResponse;
        }
    }

    public async Task<ServiceResponse> UpdateShelterAsync(UpdateShelterModel model)
    {
        var serviceResponse = new ServiceResponse();

        try
        {
            var shelterDb = await _shelterRepository.GetShelterAsync(model.ShelterId);

            if (shelterDb == null)
            {
                serviceResponse.SetFaultyState(HttpStatusCode.NotFound, Resource.ShelterNotFoundMessage);
                return serviceResponse;
            }

            var shelterByNameDb = await _shelterRepository.GetShelterByNameAsync(model.ShelterName);

            if (shelterByNameDb != null)
            {
                serviceResponse.SetFaultyState(HttpStatusCode.BadRequest, Resource.ShelterAlreadyExistsMessage);
                return serviceResponse;
            }

            shelterDb.Name = model.ShelterName;
            shelterDb.Capacity = model.Capacity;
            shelterDb.Contact.Cellphone = model.Cellphone;
            shelterDb.Contact.Address = model.Address;

            await _shelterRepository.SaveChangesAsync();

            var shelter = new ShelterModel
            {
                ShelterId = shelterDb.Id,
                ShelterName = shelterDb.Name,
                ContactId = shelterDb.ContactId,
                Cellphone = shelterDb.Contact.Cellphone,
                Email = shelterDb.Contact.Email,
                Address = shelterDb.Contact.Address,
                Capacity = shelterDb.Capacity
            };

            serviceResponse.Data = shelter;

            return serviceResponse;
        }
        catch (Exception ex)
        {
            serviceResponse.SetFaultyState(HttpStatusCode.InternalServerError, ex);
            return serviceResponse;
        }
    }
}
