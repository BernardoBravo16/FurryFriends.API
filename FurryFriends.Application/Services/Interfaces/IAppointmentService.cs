using FurryFriends.Application.Services.Models;
using FurryFriends.Application.Shared.Contracts.Security;
using FurryFriends.Application.Shared.Models.Base;

namespace FurryFriends.Application.Services.Interfaces;

public interface IAppointmentService : IAuthenticatedUserContext
{
    Task<ServiceResponse> GetAppointmentAsync(int id);
    Task<ServiceResponse> GetAppointmentsAsync();
    Task<ServiceResponse> GetAppointmentReasonsAsync();
    Task<ServiceResponse> CreateAppointmentAsync(CreateAppointmentModel model);
    Task<ServiceResponse> UpdateAppointmentAsync(UpdateAppointmentModel model);
    Task<ServiceResponse> DeleteAppointmentAsync(int id);
}