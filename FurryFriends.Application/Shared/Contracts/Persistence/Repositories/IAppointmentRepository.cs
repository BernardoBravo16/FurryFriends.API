using FurryFriends.Domain;

namespace FurryFriends.Application.Shared.Contracts.Persistence.Repositories;

public interface IAppointmentRepository
{
    Task<Appointment> GetAppointmentAsync(int id);
    Task<ICollection<Appointment>> GetAppointmentsAsync();
    Task<ICollection<Appointment>> GetAppointmentsByDateRangeAsync(DateOnly date, TimeOnly hourFrom, TimeOnly hourTo);
    Task CreateAppointmentAsync(Appointment appointment);
    Task SaveChangesAsync();
}