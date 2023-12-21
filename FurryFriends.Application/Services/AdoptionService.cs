using FurryFriends.Application.Services.Interfaces;
using FurryFriends.Application.Services.Models;
using FurryFriends.Application.Shared;
using FurryFriends.Application.Shared.Contracts.Persistence.Repositories;
using FurryFriends.Application.Shared.Models.Base;
using FurryFriends.Application.Shared.Resources;
using FurryFriends.Domain;
using System.Net;

namespace FurryFriends.Application.Services;

public class AdoptionService : BaseService, IAdoptionService
{
    private readonly IAdoptionRepository _adoptionRepository;
    private readonly IAnimalRepository _animalRepository;
    private readonly IPersonRepository _personRepository;

    public AdoptionService(IAdoptionRepository adoptionRepository,
        IAnimalRepository animalRepository,
        IPersonRepository personRepository)
    {
        _adoptionRepository = adoptionRepository;
        _animalRepository = animalRepository;
        _personRepository = personRepository;
    }

    public async Task<ServiceResponse> GetAdoptionAsync(int id)
    {
        var serviceResponse = new ServiceResponse();

        try
        {
            var adoptionDb = await _adoptionRepository.GetAdoptionAsync(id);

            if (adoptionDb == null)
            {
                serviceResponse.SetFaultyState(HttpStatusCode.NotFound, Resource.AdoptionNotFoundMessage);
                return serviceResponse;
            }

            var adoption = new AdoptionModel
            {
                AdoptionId = adoptionDb.Id,
                AnimalId = adoptionDb.AnimalId,
                Animal = adoptionDb.Animal.Name,
                PersonId = adoptionDb.PersonId,
                Person = adoptionDb.Person.Name,
                Adoption_Date = adoptionDb.Adoption_Date
            };

            serviceResponse.Data = adoption;

            return serviceResponse;
        }
        catch (Exception ex)
        {
            serviceResponse.SetFaultyState(HttpStatusCode.InternalServerError, ex);
            return serviceResponse;
        }
    }

    public async Task<ServiceResponse> GetAdoptionsByAnimalIdAsync(int animalId)
    {
        var serviceResponse = new ServiceResponse();

        try
        {
            var adoptionsDb = await _adoptionRepository.GetAdoptionsByAnimalIdAsync(animalId);

            if (adoptionsDb == null)
            {
                serviceResponse.SetFaultyState(HttpStatusCode.NotFound, Resource.AdoptionsByAnimalIdNotFoundMessage);
                return serviceResponse;
            }

            var adoptions = adoptionsDb.Select(x => new AdoptionModel
            {
                AdoptionId = x.Id,
                AnimalId = x.AnimalId,
                Animal = x.Animal.Name,
                PersonId = x.PersonId,
                Person = x.Person.Name,
                Adoption_Date = x.Adoption_Date
            });

            serviceResponse.Data = adoptions;

            return serviceResponse;
        }
        catch (Exception ex)
        {
            serviceResponse.SetFaultyState(HttpStatusCode.InternalServerError, ex);
            return serviceResponse;
        }
    }

    public async Task<ServiceResponse> GetAdoptionsByPersonIdAsync(int personId)
    {
        var serviceResponse = new ServiceResponse();

        try
        {
            var adoptionsDb = await _adoptionRepository.GetAdoptionsByPersonIdAsync(personId);

            if (adoptionsDb == null)
            {
                serviceResponse.SetFaultyState(HttpStatusCode.NotFound, Resource.AdoptionsByPersonIdNotFoundMessage);
                return serviceResponse;
            }

            var adoptions = adoptionsDb.Select(x => new AdoptionModel
            {
                AdoptionId = x.Id,
                AnimalId = x.AnimalId,
                Animal = x.Animal.Name,
                PersonId = x.PersonId,
                Person = x.Person.Name,
                Adoption_Date = x.Adoption_Date
            });

            serviceResponse.Data = adoptions;

            return serviceResponse;
        }
        catch (Exception ex)
        {
            serviceResponse.SetFaultyState(HttpStatusCode.InternalServerError, ex);
            return serviceResponse;
        }
    }

    public async Task<ServiceResponse> CreateAdoptionAsync(CreateAdoptionModel model)
    {
        var serviceResponse = new ServiceResponse();

        try
        {
            var adoptions = await _adoptionRepository.GetAdoptionsByAnimalIdAsync(model.AnimalId);
            adoptions = adoptions.Where(x => x.PersonId == model.PersonId).ToList();

            if (adoptions.Any())
            {
                serviceResponse.SetFaultyState(HttpStatusCode.BadRequest, Resource.AdoptionAlreadyExistsMessage);
                return serviceResponse;
            }

            var animal = await _animalRepository.GetAnimalAsync(model.AnimalId);

            if (animal == null)
            {
                serviceResponse.SetFaultyState(HttpStatusCode.NotFound, Resource.AnimalNotFoundMessage);
                return serviceResponse;
            }

            var person = await _personRepository.GetPersonAsync(model.PersonId);

            if (person == null)
            {
                serviceResponse.SetFaultyState(HttpStatusCode.NotFound, Resource.PersonNotFoundMessage);
                return serviceResponse;
            }

            var adoption = new Adoption
            {
                AnimalId = model.AnimalId,
                PersonId = model.PersonId,
                Adoption_Date = model.Adoption_Date
            };

            await _adoptionRepository.CreateAdoptionAsync(adoption);

            var adoptionCreated = new AdoptionModel
            {
                AdoptionId = adoption.Id,
                AnimalId = adoption.AnimalId,
                Animal = adoption.Animal.Name,
                PersonId = adoption.PersonId,
                Person = person.Name,
                Adoption_Date = adoption.Adoption_Date
            };

            serviceResponse.Data = adoptionCreated;

            return serviceResponse;
        }
        catch (Exception ex)
        {
            serviceResponse.SetFaultyState(HttpStatusCode.InternalServerError, ex);
            return serviceResponse;
        }
    }
}