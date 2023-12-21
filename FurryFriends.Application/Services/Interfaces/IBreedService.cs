using FurryFriends.Application.Shared.Models.Base;

namespace FurryFriends.Application.Services.Interfaces;

public interface IBreedService
{
    Task<ServiceResponse> GetBreedAsync(int id);
    Task<ServiceResponse> GetBreedsAsync();
}
