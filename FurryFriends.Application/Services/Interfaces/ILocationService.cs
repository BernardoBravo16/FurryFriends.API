using FurryFriends.Application.Services.Models;
using FurryFriends.Application.Shared.Contracts.Security;
using FurryFriends.Application.Shared.Models.Base;

namespace FurryFriends.Application.Services.Interfaces;

public interface ILocationService : IAuthenticatedUserContext
{
    Task<ServiceResponse> GetLocationAsync(int id);
    Task<ServiceResponse> GetLocationsByAnimalIdAsync(int animalId);
    Task<ServiceResponse> GetLocationsByShelterIdAsync(int shelterId);
    Task<ServiceResponse> CreateLocationAsync(CreateLocationModel model);
}