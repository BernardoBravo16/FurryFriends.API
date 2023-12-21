using FurryFriends.Application.Services.Models;
using FurryFriends.Application.Shared.Contracts.Security;
using FurryFriends.Application.Shared.Models.Base;

namespace FurryFriends.Application.Services.Interfaces;

public interface IAdoptionService : IAuthenticatedUserContext
{
    Task<ServiceResponse> GetAdoptionAsync(int id);
    Task<ServiceResponse> GetAdoptionsByAnimalIdAsync(int animalId);
    Task<ServiceResponse> GetAdoptionsByPersonIdAsync(int personId);
    Task<ServiceResponse> CreateAdoptionAsync(CreateAdoptionModel model);
}