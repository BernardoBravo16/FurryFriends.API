using FurryFriends.Application.Services.Authentication.Models;
using FurryFriends.Application.Shared.Models.Base;

namespace FurryFriends.Application.Services.Authentication.Interfaces;

public interface IAuthenticationService
{
    Task<ServiceResponse> GenerateAuthenticationTokenAsync(CredentialsModel model);
    Task<ServiceResponse> RegisterUserAsync(RegisterUserModel model);
    Task<ServiceResponse> ChangeUserPasswordAsync(ChangePasswordModel model);
}