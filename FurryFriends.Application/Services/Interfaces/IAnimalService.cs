using FurryFriends.Application.Services.Models;
using FurryFriends.Application.Shared.Contracts.Security;
using FurryFriends.Application.Shared.Models.Base;

namespace FurryFriends.Application.Services.Interfaces;

public interface IAnimalService : IAuthenticatedUserContext
{
    Task<ServiceResponse> GetAnimalAsync(int id);
    Task<ServiceResponse> GetAnimalsAsync();
    Task<ServiceResponse> CreateAnimalAsync(CreateAnimalModel model);
    Task<ServiceResponse> UpdateAnimalAsync(UpdateAnimalModel model);
}
