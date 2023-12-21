using FurryFriends.Application.Services.Models;
using FurryFriends.Application.Shared.Contracts.Security;
using FurryFriends.Application.Shared.Models.Base;

namespace FurryFriends.Application.Services.Interfaces;

public interface IPersonService : IAuthenticatedUserContext
{
    Task<ServiceResponse> GetPersonAsync(int id);
    Task<ServiceResponse> GetPersonsAsync();
    Task<ServiceResponse> GetPersonsNotUsersAsync();
    Task<ServiceResponse> GetPersonsByPersonRoleAsync(string role);
    Task<ServiceResponse> CreatePersonAsync(CreatePersonModel model);
    Task<ServiceResponse> UpdatePersonAsync(UpdatePersonModel model);
}
