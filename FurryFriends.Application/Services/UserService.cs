using FurryFriends.Application.Services.Interfaces;
using FurryFriends.Application.Shared.Contracts.Persistence.Repositories;
using FurryFriends.Application.Shared.Models.Base;
using FurryFriends.Domain.Enum;
using FurryFriends.Domain;
using System.Net;
using FurryFriends.Application.Services.Authentication.Interfaces;
using FurryFriends.Application.Shared;
using FurryFriends.Application.Shared.Resources;
using FurryFriends.Application.Services.Models;
using System.Data;
using System;

namespace FurryFriends.Application.Services;

public class UserService : BaseService, IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IPersonRepository _personRepository;
    private readonly IContactRepository _contactRepository;
    private readonly IEncryptionService _encryptionService;

    public UserService(IUserRepository userRepository,
        IPersonRepository personRepository,
        IContactRepository contactRepository,
        IEncryptionService encryptionService)
    {
        _userRepository = userRepository;
        _personRepository = personRepository;
        _contactRepository = contactRepository;
        _encryptionService = encryptionService;
    }

    public async Task<ServiceResponse> GetUserAsync(int id)
    {
        var serviceResponse = new ServiceResponse();

        try
        {
            var userDb = await _userRepository.GetUserAsync(id);

            if (userDb == null)
            {
                serviceResponse.SetFaultyState(HttpStatusCode.NotFound, Resource.UserNotFoundMessage);
                return serviceResponse;
            }

            var user = new UserModel
            {
                UserId = userDb.Id,
                Username = userDb.Username,
                Email = userDb.Person.Contact.Email,
                RoleId = userDb.RoleId,
                RoleName = userDb.Role.Name,
                UserStatus = userDb.Status,
                PersonId = userDb.PersonId,
                PersonTypeId = userDb.Person.PersonTypeId,
                PersonType = userDb.Person.PersonType.Name,
                PersonRoleId = userDb.Person.PersonRoleId,
                PersonRole = userDb.Person.PersonRole.Name,
                IdentityCard = userDb.Person.IdentityCard,
                Name = userDb.Person.Name,
                Surname = userDb.Person.Surname,
                Cellphone = userDb.Person.Contact.Cellphone,
                Address = userDb.Person.Contact.Address,
                Birthday = userDb.Person.Birthday,
                Gender = userDb.Person.Gender
            };

            serviceResponse.Data = user;

            return serviceResponse;
        }
        catch (Exception ex)
        {
            serviceResponse.SetFaultyState(HttpStatusCode.InternalServerError, ex);
            return serviceResponse;
        }
    }

    public async Task<ServiceResponse> GetUsersAsync()
    {
        var serviceResponse = new ServiceResponse();

        try
        {
            var usersDb = await _userRepository.GetUsersAsync();

            if (usersDb == null)
            {
                serviceResponse.SetFaultyState(HttpStatusCode.NotFound, Resource.UsersNotFoundMessage);
                return serviceResponse;
            }

            var users = usersDb.Select(x => new UserModel
            {
                UserId = x.Id,
                Username = x.Username,
                Email = x.Person.Contact.Email,
                RoleId = x.RoleId,
                RoleName = x.Role.Name,
                UserStatus = x.Status,
                PersonId = x.PersonId,
                PersonTypeId = x.Person.PersonTypeId,
                PersonType = x.Person.PersonType.Name,
                PersonRoleId = x.Person.PersonRoleId,
                PersonRole = x.Person.PersonRole.Name,
                IdentityCard = x.Person.IdentityCard,
                Name = x.Person.Name,
                Surname = x.Person.Surname,
                Cellphone = x.Person.Contact.Cellphone,
                Address = x.Person.Contact.Address,
                Birthday = x.Person.Birthday,
                Gender = x.Person.Gender
            });

            serviceResponse.Data = users;

            return serviceResponse;
        }
        catch (Exception ex)
        {
            serviceResponse.SetFaultyState(HttpStatusCode.InternalServerError, ex);
            return serviceResponse;
        }
    }

    public async Task<ServiceResponse> CreateUserAsync(CreateUserModel model)
    {
        var serviceResponse = new ServiceResponse();

        try
        {
            var userDb = await _userRepository.GetUserByUsernameAsync(model.Username);

            if (userDb != null)
            {
                serviceResponse.SetFaultyState(HttpStatusCode.BadRequest, Resource.UserAlreadyExistsMessage);
                return serviceResponse;
            }

            var personDb = await _personRepository.GetPersonByIdentityCardAsync(model.IdentityCard);

            if (personDb != null)
            {
                var userPersonDb = await _userRepository.GetUserByPersonIdAsync(personDb.Id);

                if (userPersonDb != null)
                {
                    serviceResponse.SetFaultyState(HttpStatusCode.BadRequest, Resource.PersonIsAlreadyAssociatedWithUserMessage);
                    return serviceResponse;
                }
            }

            var contactDb = await _contactRepository.GetContactByEmailAsync(model.Email);

            if (contactDb != null)
            {
                serviceResponse.SetFaultyState(HttpStatusCode.BadRequest, Resource.EmailAlreadyExistsMessage);
                return serviceResponse;
            }

            var encryptedPasswordResponse = await _encryptionService.EncryptAsync(model.Password);

            var role = (RoleEnum)Enum.Parse(typeof(RoleEnum), model.Role);
            var personType = (PersonTypeEnum)Enum.Parse(typeof(PersonTypeEnum), model.PersonType);
            var personRole = (PersonRoleEnum)Enum.Parse(typeof(PersonRoleEnum), model.PersonRole);

            var user = new User
            {
                PersonId = personDb == null ? 0 : personDb.Id,
                RoleId = (int)role,
                Username = model.Username,
                Password = encryptedPasswordResponse.Data.ToString(),
                Status = true,
                Person = personDb == null ? new Person
                {
                    PersonTypeId = (int)personType,
                    PersonRoleId = (int)personRole,
                    ContactId = 0,
                    IdentityCard = model.IdentityCard,
                    Name = model.Name,
                    Surname = model.Surname,
                    Birthday = model.Birthday,
                    Gender = model.Gender,
                    Status = true,
                    Contact = new Contact
                    {
                        Cellphone = model.Cellphone,
                        Email = model.Email,
                        Address = model.Address
                    }
                } : null
            };

            await _userRepository.CreateUserAsync(user);

            var userCreated = new UserModel
            {
                UserId = user.Id,
                Username = user.Username,
                Email = personDb == null ? user.Person.Contact.Email : personDb.Contact.Email,
                RoleId = user.RoleId,
                RoleName = role.ToString(),
                UserStatus = user.Status,
                PersonId = user.PersonId,
                PersonTypeId = personDb == null ? user.Person.PersonTypeId : personDb.PersonTypeId,
                PersonType = personType.ToString(),
                PersonRoleId = personDb == null ? user.Person.PersonRoleId : personDb.PersonRoleId,
                PersonRole = personRole.ToString(),
                IdentityCard = personDb == null ? user.Person.IdentityCard : personDb.IdentityCard,
                Name = personDb == null ? user.Person.Name : personDb.Name,
                Surname = personDb == null ? user.Person.Surname : personDb.Surname,
                Cellphone = personDb == null ? user.Person.Contact.Cellphone : personDb.Contact.Cellphone,
                Address = personDb == null ? user.Person.Contact.Address : personDb.Contact.Address,
                Birthday = personDb == null ? user.Person.Birthday : personDb.Birthday,
                Gender = personDb == null ? user.Person.Gender : personDb.Gender
            };

            serviceResponse.Data = userCreated;

            return serviceResponse;
        }
        catch (Exception ex)
        {
            serviceResponse.SetFaultyState(HttpStatusCode.InternalServerError, ex);
            return serviceResponse;
        }
    }

    public async Task<ServiceResponse> CreateUserOnlyAsync(CreateUserOnlyModel model)
    {
        var serviceResponse = new ServiceResponse();

        try
        {
            var userDb = await _userRepository.GetUserByUsernameAsync(model.Username);

            if (userDb != null)
            {
                serviceResponse.SetFaultyState(HttpStatusCode.BadRequest, Resource.UserAlreadyExistsMessage);
                return serviceResponse;
            }

            var person = await _personRepository.GetPersonAsync(model.PersonId);

            if (person == null)
            {
                serviceResponse.SetFaultyState(HttpStatusCode.NotFound, Resource.PersonNotFoundMessage);
                return serviceResponse;
            }

            var encryptedPasswordResponse = await _encryptionService.EncryptAsync(model.Password);

            var role = (RoleEnum)model.RoleId;

            var user = new User
            {
                PersonId = person.Id,
                RoleId = (int)role,
                Username = model.Username,
                Password = encryptedPasswordResponse.Data.ToString(),
                Status = model.Status,
            };

            await _userRepository.CreateUserAsync(user);

            var userCreated = new CreateUserOnlyModel
            {
                Username = model.Username,
                RoleId = model.RoleId,
                Status = model.Status,
                Password = model.Password,
                PersonId = model.PersonId
            };

            serviceResponse.Data = userCreated;

            return serviceResponse;
        }
        catch (Exception ex)
        {
            serviceResponse.SetFaultyState(HttpStatusCode.InternalServerError, ex);
            return serviceResponse;
        }
    }

    public async Task<ServiceResponse> UpdateUserAsync(UpdateUserModel model)
    {
        var serviceResponse = new ServiceResponse();

        try
        {
            var userDb = await _userRepository.GetUserAsync(model.UserId);

            if (userDb == null)
            {
                serviceResponse.SetFaultyState(HttpStatusCode.NotFound, Resource.UserNotFoundMessage);
                return serviceResponse;
            }

            var role = (RoleEnum)Enum.Parse(typeof(RoleEnum), model.Role);
            var personType = (PersonTypeEnum)Enum.Parse(typeof(PersonTypeEnum), model.PersonType);
            var personRole = (PersonRoleEnum)Enum.Parse(typeof(PersonRoleEnum), model.PersonRole);

            userDb.RoleId = (int)role;
            userDb.Status = model.UserStatus;
            userDb.Person.PersonTypeId = (int)personType;
            userDb.Person.PersonRoleId = (int)personRole;
            userDb.Person.Name = model.Name;
            userDb.Person.Surname = model.Surname;
            userDb.Person.Birthday = model.Birthday;
            userDb.Person.Gender = model.Gender;
            userDb.Person.Contact.Cellphone = model.Cellphone;
            userDb.Person.Contact.Address = model.Address;

            await _userRepository.SaveChangesAsync();

            var user = new UserModel
            {
                UserId = userDb.Id,
                Username = userDb.Username,
                Email = userDb.Person.Contact.Email,
                RoleId = userDb.RoleId,
                RoleName = userDb.Role.Name,
                UserStatus = userDb.Status,
                PersonId = userDb.PersonId,
                PersonTypeId = userDb.Person.PersonTypeId,
                PersonType = userDb.Person.PersonType.Name,
                PersonRoleId = userDb.Person.PersonRoleId,
                PersonRole = userDb.Person.PersonRole.Name,
                IdentityCard = userDb.Person.IdentityCard,
                Name = userDb.Person.Name,
                Surname = userDb.Person.Surname,
                Cellphone = userDb.Person.Contact.Cellphone,
                Address = userDb.Person.Contact.Address,
                Birthday = userDb.Person.Birthday,
                Gender = userDb.Person.Gender
            };

            serviceResponse.Data = user;

            return serviceResponse;
        }
        catch (Exception ex)
        {
            serviceResponse.SetFaultyState(HttpStatusCode.InternalServerError, ex);
            return serviceResponse;
        }
    }

    public async Task<ServiceResponse> UpdateUserOnlyAsync(UpdateUserOnlyModel model)
    {
        var serviceResponse = new ServiceResponse();

        try
        {
            var userDb = await _userRepository.GetUserAsync(model.UserId);

            if (userDb == null)
            {
                serviceResponse.SetFaultyState(HttpStatusCode.NotFound, Resource.UserNotFoundMessage);
                return serviceResponse;
            }

            var role = (RoleEnum)model.RoleId;
            var encryptedPasswordResponse = await _encryptionService.EncryptAsync(model.Password);

            userDb.PersonId = model.PersonId;
            userDb.RoleId = (int)role;
            userDb.Username = model.Username;
            userDb.Password = model.Password == null ? userDb.Password : encryptedPasswordResponse.Data.ToString();
            userDb.Status = model.Status;

            await _userRepository.SaveChangesAsync();

            var user = new UpdateUserOnlyModel
            {
                UserId = userDb.Id,
                Username = userDb.Username,
                PersonId = userDb.PersonId,
                Password = model.Password,
            };

            serviceResponse.Data = user;

            return serviceResponse;
        }
        catch (Exception ex)
        {
            serviceResponse.SetFaultyState(HttpStatusCode.InternalServerError, ex);
            return serviceResponse;
        }
    }
}