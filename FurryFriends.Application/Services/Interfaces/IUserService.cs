using FurryFriends.Application.Services.Models;
using FurryFriends.Application.Shared.Contracts.Security;
using FurryFriends.Application.Shared.Models.Base;

namespace FurryFriends.Application.Services.Interfaces;

public interface IUserService : IAuthenticatedUserContext
{
    Task<ServiceResponse> GetUserAsync(int id);
    Task<ServiceResponse> GetUsersAsync();
    Task<ServiceResponse> CreateUserAsync(CreateUserModel model);
    Task<ServiceResponse> CreateUserOnlyAsync(CreateUserOnlyModel model);
    Task<ServiceResponse> UpdateUserAsync(UpdateUserModel model);
    Task<ServiceResponse> UpdateUserOnlyAsync(UpdateUserOnlyModel model);
}
