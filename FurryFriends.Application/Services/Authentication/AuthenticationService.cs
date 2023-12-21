using FurryFriends.Application.Shared.Contracts.Persistence.Repositories;
using FurryFriends.Application.Shared.Models.Base;
using FurryFriends.Application.Shared.Resources;
using System.Net;
using FurryFriends.Application.Services.Authentication.Interfaces;
using FurryFriends.Domain;
using FurryFriends.Domain.Enum;
using FurryFriends.Application.Services.Authentication.Models;

namespace FurryFriends.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly ITokenService _tokenService;
    private readonly IEncryptionService _encryptionService;
    private readonly IUserRepository _userRepository;

    public AuthenticationService(ITokenService tokenService,
        IEncryptionService encryptionService,
        IUserRepository userRepository)
    {
        _tokenService = tokenService;
        _encryptionService = encryptionService;
        _userRepository = userRepository;
    }

    public async Task<ServiceResponse> GenerateAuthenticationTokenAsync(CredentialsModel model)
    {
        var serviceResponse = new ServiceResponse();

        try
        {
            var user = await _userRepository.GetUserByUsernameAsync(model.Username);

            if (user == null)
            {
                serviceResponse.SetFaultyState(HttpStatusCode.NotFound, Resource.UserNotFoundMessage);
                return serviceResponse;
            }

            if (!user.Status)
            {
                serviceResponse.SetFaultyState(HttpStatusCode.Unauthorized, Resource.DisableUserMessage);
                return serviceResponse;
            }

            var encryptedPasswordResponse = await _encryptionService.EncryptAsync(model.Password);

            if (!user.Password.Equals(encryptedPasswordResponse.Data))
            {
                serviceResponse.SetFaultyState(HttpStatusCode.Unauthorized, Resource.InvalidCredentialsMessage);
                return serviceResponse;
            }

            if (user.Role == null || !user.Role.Status)
            {
                serviceResponse.SetFaultyState(HttpStatusCode.Unauthorized, Resource.DisableUserRoleMessage);
                return serviceResponse;
            }

            var authenticatedUser = new AuthenticatedUserModel
            {
                UserId = user.Id,
                Username = user.Username,
                Email = user.Person.Contact.Email,
                PersonId = user.PersonId,
                PersonTypeId = user.Person.PersonTypeId,
                PersonType = user.Person.PersonType.Name,
                PersonRoleId = user.Person.PersonRoleId,
                PersonRole = user.Person.PersonRole.Name,
                IdentityCard = user.Person.IdentityCard,
                Name = user.Person.Name,
                Surname = user.Person.Surname,
                RoleId = user.RoleId,
                RoleName = user.Role.Name
            };

            var tokenResponse = await _tokenService.GenerateTokenAsync(authenticatedUser);

            serviceResponse.Data = new
            {
                AuthenticatedUser = authenticatedUser,
                Token = tokenResponse.Data
            };

            return serviceResponse;
        }
        catch (Exception ex)
        {
            serviceResponse.SetFaultyState(HttpStatusCode.InternalServerError, ex);
            return serviceResponse;
        }
    }

    public async Task<ServiceResponse> RegisterUserAsync(RegisterUserModel model)
    {
        var serviceResponse = new ServiceResponse();

        try
        {
            var encryptedPasswordResponse = await _encryptionService.EncryptAsync(model.Password);

            var user = new User
            {
                PersonId = 0,
                RoleId = (int)RoleEnum.Client,
                Username = model.Username,
                Password = encryptedPasswordResponse.Data.ToString(),
                Status = true,
                Person = new Person
                {
                    PersonTypeId = (int)PersonTypeEnum.Client,
                    PersonRoleId = (int)PersonRoleEnum.None,
                    ContactId = 0,
                    IdentityCard = model.IdentityCard,
                    Name = model.Name,
                    Surname = model.Surname,
                    Birthday = model.Birthday,
                    Gender = model.Gender,
                    Status = true,
                    Contact = new Contact
                    {
                        Cellphone = model.CellPhone,
                        Email = model.Email,
                        Address = model.Address
                    }
                }
            };

            await _userRepository.CreateUserAsync(user);

            var createdUser = new AuthenticatedUserModel
            {
                UserId = user.Id,
                Username = user.Username,
                Email = user.Person.Contact.Email,
                PersonTypeId = (int)PersonTypeEnum.Client,
                PersonType = PersonTypeEnum.Client.ToString(),
                PersonRoleId = (int)PersonRoleEnum.None,
                PersonRole = PersonRoleEnum.None.ToString(),
                IdentityCard = user.Person.IdentityCard,
                Name = user.Person.Name,
                Surname = user.Person.Surname,
                RoleId = (int)RoleEnum.Client,
                RoleName = RoleEnum.Client.ToString()
            };

            var tokenResponse = await _tokenService.GenerateTokenAsync(createdUser);

            serviceResponse.Data = new
            {
                AuthenticatedUser = createdUser,
                Token = tokenResponse.Data
            };

            return serviceResponse;
        }
        catch (Exception ex)
        {
            serviceResponse.SetFaultyState(HttpStatusCode.InternalServerError, ex);
            return serviceResponse;
        }
    }

    public async Task<ServiceResponse> ChangeUserPasswordAsync(ChangePasswordModel model)
    {
        var serviceResponse = new ServiceResponse();

        try
        {
            var user = await _userRepository.GetUserAsync(model.UserId);

            if (user == null)
            {
                serviceResponse.SetFaultyState(HttpStatusCode.NotFound, Resource.UserNotFoundMessage);
                return serviceResponse;
            }

            var encryptedOldPasswordResponse = await _encryptionService.EncryptAsync(model.OldPassword);

            if (!user.Password.Equals(encryptedOldPasswordResponse.Data))
            {
                serviceResponse.SetFaultyState(HttpStatusCode.Unauthorized, Resource.InvalidCredentialsMessage);
                return serviceResponse;
            }

            if (model.NewPassword != model.ConfirmNewPassword)
            {
                serviceResponse.SetFaultyState(HttpStatusCode.Unauthorized, Resource.InvalidNewPasswordConfirmedMessage);
                return serviceResponse;
            }

            var encryptedPasswordResponse = await _encryptionService.EncryptAsync(model.NewPassword);

            user.Password = encryptedPasswordResponse.Data.ToString();

            await _userRepository.SaveChangesAsync();

            return serviceResponse;
        }
        catch (Exception ex)
        {
            serviceResponse.SetFaultyState(HttpStatusCode.InternalServerError, ex);
            return serviceResponse;
        }
    }
}