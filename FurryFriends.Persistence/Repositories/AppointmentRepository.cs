using FurryFriends.Application.Shared.Contracts.Persistence;
using FurryFriends.Application.Shared.Contracts.Persistence.Repositories;
using FurryFriends.Domain;
using FurryFriends.Domain.Enum;
using Microsoft.EntityFrameworkCore;

namespace FurryFriends.Persistence.Repositories;

public class AppointmentRepository : IAppointmentRepository
{
    private readonly IRepository<Appointment, int> _appointmentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AppointmentRepository(IRepository<Appointment, int> appointmentRepository, IUnitOfWork unitOfWork)
    {
        _appointmentRepository = appointmentRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Appointment> GetAppointmentAsync(int id)
    {
        return await _appointmentRepository
            .GetAll()
            .Include(x => x.Person)
            .Include(x => x.AppointmentReason)
            .Include(x => x.Shelter)
            .Include(x => x.AppointmentStatus)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<ICollection<Appointment>> GetAppointmentsAsync()
    {
        return await _appointmentRepository
            .GetAll()
            .Include(x => x.Person)
            .Include(x => x.AppointmentReason)
            .Include(x => x.Shelter)
            .Include(x => x.AppointmentStatus)
            .Where(x => (AppointmentStatusEnum)x.AppointmentStatusId != AppointmentStatusEnum.Deleted)
            .OrderByDescending(x => x.AppointmentDate)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<ICollection<Appointment>> GetAppointmentsByDateRangeAsync(
    DateOnly date, TimeOnly hourFrom, TimeOnly hourTo)
    {
        return await _appointmentRepository
            .GetAll()
            .AsNoTracking()
            .Where(x =>
                x.AppointmentDate == date &&
                x.AppointmentHour >= hourFrom &&
                x.AppointmentHour <= hourTo &&
                x.AppointmentStatusId == (int)AppointmentStatusEnum.Scheduled)
            .ToListAsync();
    }

    public async Task CreateAppointmentAsync(Appointment appointment)
    {
        await _appointmentRepository.AddAsync(appointment);

        await SaveChangesAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _unitOfWork.SaveAsync();
    }
}