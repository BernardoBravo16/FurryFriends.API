using FurryFriends.Application.Shared.Models.Base;

namespace FurryFriends.Application.Services.Authentication.Interfaces;

public interface IEncryptionService
{
    Task<ServiceResponse> EncryptAsync(string content);
}