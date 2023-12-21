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

public class AnimalService : BaseService, IAnimalService
{

    private readonly IAnimalRepository _animalRepository;
    private readonly IBreedRepository _breedRepository;

    public AnimalService(IAnimalRepository animalRepository,
        IBreedRepository breedRepository)
    {
        _animalRepository = animalRepository;
        _breedRepository = breedRepository;
    }

    public async Task<ServiceResponse> GetAnimalAsync(int id)
    {
        var serviceResponse = new ServiceResponse();

        try
        {
            var animalDb = await _animalRepository.GetAnimalAsync(id);

            if (animalDb == null)
            {
                serviceResponse.SetFaultyState(HttpStatusCode.NotFound, Resource.AnimalNotFoundMessage);
                return serviceResponse;
            }

            var animal = new AnimalModel
            {
                AnimalId = animalDb.Id,
                AnimalTypeId = animalDb.AnimalTypeId,
                AnimalType = animalDb.AnimalType.Name,
                BreedId = animalDb.BreedId,
                Breed = animalDb.Breed.Name,
                Name = animalDb.Name,
                Age = animalDb.Age,
                AgeType = animalDb.AgeType,
                Its_Alive = animalDb.Its_Alive,
                Is_Adopted = animalDb.Is_Adopted
            };

            serviceResponse.Data = animal;

            return serviceResponse;
        }
        catch (Exception ex)
        {
            serviceResponse.SetFaultyState(HttpStatusCode.InternalServerError, ex);
            return serviceResponse;
        }
    }

    public async Task<ServiceResponse> GetAnimalsAsync()
    {
        var serviceResponse = new ServiceResponse();

        try
        {

            var animalsDb = await _animalRepository.GetAnimalsAsync();

            if (animalsDb == null)
            {
                serviceResponse.SetFaultyState(HttpStatusCode.NotFound, Resource.AnimalNotFoundMessage);
                return serviceResponse;
            }

            var animals = animalsDb.Select(x => new AnimalModel
            {
                AnimalId = x.Id,
                AnimalTypeId = x.AnimalTypeId,
                AnimalType = x.AnimalType.Name,
                BreedId = x.BreedId,
                Breed = x.Breed.Name,
                IdentityNumber = x.IdentityNumber,
                Name = x.Name,
                Age = x.Age,
                AgeType = x.AgeType,
                Its_Alive = x.Its_Alive,
                Is_Adopted = x.Is_Adopted

            });

            serviceResponse.Data = animals;

            return serviceResponse;
        }
        catch (Exception ex)
        {
            serviceResponse.SetFaultyState(HttpStatusCode.InternalServerError, ex);
            return serviceResponse;
        }
    }

    public async Task<ServiceResponse> CreateAnimalAsync(CreateAnimalModel model)
    {
        var serviceResponse = new ServiceResponse();

        try
        {
            var animalDb = await _animalRepository.GetAnimalByIdentityNumberAsync(model.IdentityNumber);

            if (animalDb != null)
            {
                serviceResponse.SetFaultyState(HttpStatusCode.BadRequest, Resource.AnimalAlreadyExistsMessage);
                return serviceResponse;
            }

            var breedDb = await _breedRepository.GetBreedAsync(model.BreedId);

            if (breedDb == null)
            {
                serviceResponse.SetFaultyState(HttpStatusCode.NotFound, Resource.BreedNotFoundMessage);
                return serviceResponse;
            }

            var animal = new Animal
            {
                AnimalTypeId = model.AnimalTypeId,
                BreedId = model.BreedId,
                IdentityNumber = model.IdentityNumber,
                Name = model.Name,
                Age = model.Age,
                AgeType = model.AgeType,
                Its_Alive = model.Its_Alive,
                Is_Adopted = model.Is_Adopted
            };

            await _animalRepository.CreateAnimalAsync(animal);

            var animalCreated = new AnimalModel
            {
                AnimalId = animal.Id,
                AnimalTypeId = animal.AnimalTypeId,
                AnimalType = ((AnimalTypeEnum)animal.AnimalTypeId).ToString(),
                BreedId = animal.BreedId,
                Breed = animal.Breed.Name,
                Name = animal.Name,
                Age = animal.Age,
                AgeType = animal.AgeType,
                Its_Alive = animal.Its_Alive,
                Is_Adopted = animal.Is_Adopted
            };

            serviceResponse.Data = animalCreated;

            return serviceResponse;
        }
        catch (Exception ex)
        {
            serviceResponse.SetFaultyState(HttpStatusCode.InternalServerError, ex);
            return serviceResponse;
        }
    }

    public async Task<ServiceResponse> UpdateAnimalAsync(UpdateAnimalModel model)
    {
        var serviceResponse = new ServiceResponse();

        try
        {
            var animalDb = await _animalRepository.GetAnimalAsync(model.AnimalId);

            if (animalDb == null)
            {
                serviceResponse.SetFaultyState(HttpStatusCode.NotFound, Resource.AnimalNotFoundMessage);
                return serviceResponse;
            }

            animalDb.Age = model.Age;
            animalDb.AgeType = model.AgeType;
            animalDb.Its_Alive = model.Its_Alive;
            animalDb.Is_Adopted = model.Is_Adopted;

            await _animalRepository.SaveChangesAsync();


            var animal = new AnimalModel
            {
                AnimalId = animalDb.Id,
                AnimalTypeId = animalDb.AnimalTypeId,
                AnimalType = animalDb.AnimalType.Name,
                BreedId = animalDb.BreedId,
                Breed = animalDb.Breed.Name,
                Name = animalDb.Name,
                Age = animalDb.Age,
                AgeType = animalDb.AgeType,
                Its_Alive = animalDb.Its_Alive,
                Is_Adopted = animalDb.Is_Adopted
            };

            serviceResponse.Data = animal;

            return serviceResponse;
        }
        catch (Exception ex)
        {
            serviceResponse.SetFaultyState(HttpStatusCode.InternalServerError, ex);
            return serviceResponse;
        }
    }
}
