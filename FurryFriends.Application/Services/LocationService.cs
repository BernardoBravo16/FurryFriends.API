using FurryFriends.Application.Services.Interfaces;
using FurryFriends.Application.Services.Models;
using FurryFriends.Application.Shared;
using FurryFriends.Application.Shared.Contracts.Persistence.Repositories;
using FurryFriends.Application.Shared.Models.Base;
using FurryFriends.Application.Shared.Resources;
using FurryFriends.Domain;
using System.Net;

namespace FurryFriends.Application.Services;

public class LocationService : BaseService, ILocationService
{
    private readonly ILocationRepository _locationRepository;
    private readonly IAnimalRepository _animalRepository;
    private readonly IShelterRepository _shelterRepository;

    public LocationService(ILocationRepository locationRepository,
        IAnimalRepository animalRepository,
        IShelterRepository shelterRepository)
    {
        _locationRepository = locationRepository;
        _animalRepository = animalRepository;
        _shelterRepository = shelterRepository;
    }

    public async Task<ServiceResponse> GetLocationAsync(int id)
    {
        var serviceResponse = new ServiceResponse();

        try
        {
            var locationDb = await _locationRepository.GetLocationAsync(id);

            if (locationDb == null)
            {
                serviceResponse.SetFaultyState(HttpStatusCode.NotFound, Resource.LocationNotFoundMessage);
                return serviceResponse;
            }

            var location = new LocationModel
            {
                LocationId = locationDb.Id,
                AnimalId = locationDb.AnimalId,
                Animal = locationDb.Animal.Name,
                ShelterId = locationDb.ShelterId,
                Shelter = locationDb.Shelter.Name,
                Date_From = locationDb.Date_From,
                Date_To = locationDb.Date_To,
                Status = locationDb.Status
            };

            serviceResponse.Data = location;

            return serviceResponse;
        }
        catch (Exception ex)
        {
            serviceResponse.SetFaultyState(HttpStatusCode.InternalServerError, ex);
            return serviceResponse;
        }
    }

    public async Task<ServiceResponse> GetLocationsByAnimalIdAsync(int animalId)
    {
        var serviceResponse = new ServiceResponse();

        try
        {
            var locationsDb = await _locationRepository.GetLocationsByAnimalIdAsync(animalId);

            if (locationsDb == null)
            {
                serviceResponse.SetFaultyState(HttpStatusCode.NotFound, Resource.LocationsByAnimalIdNotFoundMessage);
                return serviceResponse;
            }

            var locations = locationsDb.Select(x => new LocationModel
            {
                LocationId = x.Id,
                AnimalId = x.AnimalId,
                Animal = x.Animal.Name,
                ShelterId = x.ShelterId,
                Shelter = x.Shelter.Name,
                Date_From = x.Date_From,
                Date_To = x.Date_To,
                Status = x.Status
            });

            serviceResponse.Data = locations;

            return serviceResponse;
        }
        catch (Exception ex)
        {
            serviceResponse.SetFaultyState(HttpStatusCode.InternalServerError, ex);
            return serviceResponse;
        }
    }

    public async Task<ServiceResponse> GetLocationsByShelterIdAsync(int shelterId)
    {
        var serviceResponse = new ServiceResponse();

        try
        {
            var locationsDb = await _locationRepository.GetLocationsByShelterIdAsync(shelterId);

            if (locationsDb == null)
            {
                serviceResponse.SetFaultyState(HttpStatusCode.NotFound, Resource.LocationsByShelterIdNotFoundMessage);
                return serviceResponse;
            }

            var locations = locationsDb.Select(x => new LocationModel
            {
                LocationId = x.Id,
                AnimalId = x.AnimalId,
                Animal = x.Animal.Name,
                ShelterId = x.ShelterId,
                Shelter = x.Shelter.Name,
                Date_From = x.Date_From,
                Date_To = x.Date_To,
                Status = x.Status
            });

            serviceResponse.Data = locations;

            return serviceResponse;
        }
        catch (Exception ex)
        {
            serviceResponse.SetFaultyState(HttpStatusCode.InternalServerError, ex);
            return serviceResponse;
        }
    }

    public async Task<ServiceResponse> CreateLocationAsync(CreateLocationModel model)
    {
        var serviceResponse = new ServiceResponse();

        try
        {
            var locations = await _locationRepository.GetLocationsByAnimalIdAsync(model.AnimalId);

            locations = locations.Where(x => x.Date_From <= model.Date_To && x.Date_To >= model.Date_From)
                .ToList();

            if (locations.Any())
            {
                serviceResponse.SetFaultyState(HttpStatusCode.BadRequest, Resource.LocationAlreadyExistsMessage);
                return serviceResponse;
            }

            var animal = await _animalRepository.GetAnimalAsync(model.AnimalId);

            if (animal == null)
            {
                serviceResponse.SetFaultyState(HttpStatusCode.NotFound, Resource.AnimalNotFoundMessage);
                return serviceResponse;
            }

            var shelter = await _shelterRepository.GetShelterAsync(model.ShelterId);

            if (shelter == null)
            {
                serviceResponse.SetFaultyState(HttpStatusCode.NotFound, Resource.ShelterNotFoundMessage);
                return serviceResponse;
            }

            var location = new Location
            {
                AnimalId = model.AnimalId,
                ShelterId = model.ShelterId,
                Date_From = model.Date_From,
                Date_To = model.Date_To,
                Status = model.Status,
            };

            await _locationRepository.CreateLocationAsync(location);

            var locationCreated = new LocationModel
            {
                LocationId = location.Id,
                AnimalId = location.AnimalId,
                Animal = location.Animal.Name,
                ShelterId = location.ShelterId,
                Shelter = location.Shelter.Name,
                Date_From = location.Date_From,
                Date_To = location.Date_To,
                Status = location.Status
            };

            serviceResponse.Data = locationCreated;

            return serviceResponse;
        }
        catch (Exception ex)
        {
            serviceResponse.SetFaultyState(HttpStatusCode.InternalServerError, ex);
            return serviceResponse;
        }
    }
}