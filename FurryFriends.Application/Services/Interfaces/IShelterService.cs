using FurryFriends.Application.Services.Models;
using FurryFriends.Application.Shared.Contracts.Security;
using FurryFriends.Application.Shared.Models.Base;

namespace FurryFriends.Application.Services.Interfaces;

public interface IShelterService : IAuthenticatedUserContext
{
    Task<ServiceResponse> GetShelterAsync(int id);
    Task<ServiceResponse> GetSheltersAsync();
    Task<ServiceResponse> CreateShelterAsync(CreateShelterModel model);
    Task<ServiceResponse> UpdateShelterAsync(UpdateShelterModel model);
}
