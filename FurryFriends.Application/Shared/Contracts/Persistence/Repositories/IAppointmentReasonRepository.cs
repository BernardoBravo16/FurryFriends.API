using FurryFriends.Domain;

namespace FurryFriends.Application.Shared.Contracts.Persistence.Repositories;

public interface IAppointmentReasonRepository
{
    Task<AppointmentReason> GetAppointmentReasonAsync(int id);
    Task<ICollection<AppointmentReason>> GetAppointmentReasonsAsync();
}