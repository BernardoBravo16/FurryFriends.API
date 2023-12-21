using FurryFriends.Application.Shared.Contracts.Persistence;
using FurryFriends.Application.Shared.Contracts.Persistence.Repositories;
using FurryFriends.Domain;
using Microsoft.EntityFrameworkCore;

namespace FurryFriends.Persistence.Repositories;

public class AppointmentReasonRepository : IAppointmentReasonRepository
{
    private readonly IRepository<AppointmentReason, int> _appointmentReasonRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AppointmentReasonRepository(IRepository<AppointmentReason, int> appointmentReasonRepository, IUnitOfWork unitOfWork)
    {
        _appointmentReasonRepository = appointmentReasonRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<AppointmentReason> GetAppointmentReasonAsync(int id)
    {
        return await _appointmentReasonRepository
            .GetAll()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<ICollection<AppointmentReason>> GetAppointmentReasonsAsync()
    {
        return await _appointmentReasonRepository
            .GetAll()
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task CreateAppointmentReasonAsync(AppointmentReason appointmentReason)
    {
        await _appointmentReasonRepository.AddAsync(appointmentReason);

        await SaveChangesAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _unitOfWork.SaveAsync();
    }
}