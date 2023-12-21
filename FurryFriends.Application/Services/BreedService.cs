using FurryFriends.Application.Services.Interfaces;
using FurryFriends.Application.Services.Models;
using FurryFriends.Application.Shared.Contracts.Persistence.Repositories;
using FurryFriends.Application.Shared.Models.Base;
using FurryFriends.Application.Shared.Resources;
using System.Net;

namespace FurryFriends.Application.Services;

public class BreedService : IBreedService
{
    private readonly IBreedRepository _breedRepository;

    public BreedService(IBreedRepository breedRepository)
    {
        _breedRepository = breedRepository;
    }

    public async Task<ServiceResponse> GetBreedAsync(int id)
    {
        var serviceResponse = new ServiceResponse();

        try
        {
            var breedDb = await _breedRepository.GetBreedAsync(id);

            if (breedDb == null)
            {
                serviceResponse.SetFaultyState(HttpStatusCode.NotFound, Resource.BreedNotFoundMessage);
                return serviceResponse;
            }

            var breed = new BreedModel
            {
                BreedId = breedDb.Id,
                AnimalTypeId = breedDb.AnimalTypeId,
                Name = breedDb.Name,
            };

            serviceResponse.Data = breed;

            return serviceResponse;
        }
        catch (Exception ex)
        {
            serviceResponse.SetFaultyState(HttpStatusCode.InternalServerError, ex);
            return serviceResponse;
        }
    }

    public async Task<ServiceResponse> GetBreedsAsync()
    {
        var serviceResponse = new ServiceResponse();

        try
        {

            var breedsDb = await _breedRepository.GetBreedsAsync();

            if (breedsDb == null)
            {
                serviceResponse.SetFaultyState(HttpStatusCode.NotFound, Resource.BreedNotFoundMessage);
                return serviceResponse;
            }

            var breeds = breedsDb.Select(x => new BreedModel
            {
                BreedId = x.Id,
                AnimalTypeId = x.AnimalTypeId,
                Name = x.Name
            });

            serviceResponse.Data = breeds;

            return serviceResponse;
        }
        catch (Exception ex)
        {
            serviceResponse.SetFaultyState(HttpStatusCode.InternalServerError, ex);
            return serviceResponse;
        }
    }
}
