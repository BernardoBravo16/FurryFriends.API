using FurryFriends.Application.Services.Authentication.Models;
using FurryFriends.Application.Shared.Models.Base;

namespace FurryFriends.Application.Services.Authentication.Interfaces;

public interface ITokenService
{
    Task<ServiceResponse> GenerateTokenAsync(AuthenticatedUserModel authenticatedUser);
}