using FurryFriends.Application.Services.Models;
using FurryFriends.Application.Shared.Contracts.Security;
using FurryFriends.Application.Shared.Models.Base;

namespace FurryFriends.Application.Services.Interfaces;

public interface IMedicalRecordService : IAuthenticatedUserContext
{
    Task<ServiceResponse> GetMedicalRecordAsync(int id);
    Task<ServiceResponse> GetMedicalRecordsByAnimalIdAsync(int animalId);
    Task<ServiceResponse> GetMedicalRecordsByVeterinaryIdAsync(int veterinaryId);
    Task<ServiceResponse> CreateMedicalRecordAsync(CreateMedicalRecordModel model);
}