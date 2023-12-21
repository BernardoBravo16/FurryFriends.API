using FurryFriends.Application.Services.Interfaces;
using FurryFriends.Application.Services.Models;
using FurryFriends.Application.Shared;
using FurryFriends.Application.Shared.Contracts.Persistence.Repositories;
using FurryFriends.Application.Shared.Models.Base;
using FurryFriends.Application.Shared.Resources;
using FurryFriends.Domain;
using FurryFriends.Domain.Enum;
using System.Net;

namespace FurryFriends.Application.Services;

public class AppointmentService : BaseService, IAppointmentService
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IPersonRepository _personRepository;
    private readonly IAppointmentReasonRepository _appointmentReasonRepository;
    private readonly IShelterRepository _shelterRepository;

    public AppointmentService(IAppointmentRepository appointmentRepository,
        IPersonRepository personRepository,
        IAppointmentReasonRepository appointmentReasonRepository,
        IShelterRepository shelterRepository)
    {
        _appointmentRepository = appointmentRepository;
        _personRepository = personRepository;
        _appointmentReasonRepository = appointmentReasonRepository;
        _shelterRepository = shelterRepository;
    }

    public async Task<ServiceResponse> GetAppointmentAsync(int id)
    {
        var serviceResponse = new ServiceResponse();

        try
        {
            var appointmentDb = await _appointmentRepository.GetAppointmentAsync(id);

            if (appointmentDb == null || appointmentDb.PersonId != AuthPersonId)
            {
                serviceResponse.SetFaultyState(HttpStatusCode.NotFound, Resource.AppointmentNotFoundMessage);
                return serviceResponse;
            }

            var appointment = new AppointmentModel
            {
                AppointmentId = appointmentDb.Id,
                PersonId = appointmentDb.PersonId,
                Person = appointmentDb.Person.Name,
                AppointmentReasonId = appointmentDb.AppointmentReasonId,
                AppointmentReason = appointmentDb.AppointmentReason.Name,
                ShelterId = appointmentDb.ShelterId,
                Shelter = appointmentDb.Shelter.Name,
                AppointmentStatusId = appointmentDb.AppointmentStatusId,
                AppointmentStatus = appointmentDb.AppointmentStatus.Name,
                Date = appointmentDb.AppointmentDate,
                Hour = appointmentDb.AppointmentHour
            };

            serviceResponse.Data = appointment;

            return serviceResponse;
        }
        catch (Exception ex)
        {
            serviceResponse.SetFaultyState(HttpStatusCode.InternalServerError, ex);
            return serviceResponse;
        }
    }

    public async Task<ServiceResponse> GetAppointmentsAsync()
    {
        var serviceResponse = new ServiceResponse();

        try
        {
            var appointmentsDb = await _appointmentRepository.GetAppointmentsAsync();

            if (AuthRole == RoleEnum.Client)
                appointmentsDb = appointmentsDb.Where(x => x.PersonId == AuthPersonId).ToList();

            if (appointmentsDb == null)
            {
                serviceResponse.SetFaultyState(HttpStatusCode.NotFound, Resource.AppointmentsNotFoundMessage);
                return serviceResponse;
            }

            var appointments = appointmentsDb.Select(x => new AppointmentModel
            {
                AppointmentId = x.Id,
                PersonId = x.PersonId,
                Person = x.Person.Name,
                AppointmentReasonId = x.AppointmentReasonId,
                AppointmentReason = x.AppointmentReason.Name,
                ShelterId = x.ShelterId,
                Shelter = x.Shelter.Name,
                AppointmentStatusId = x.AppointmentStatusId,
                AppointmentStatus = x.AppointmentStatus.Name,
                Date = x.AppointmentDate,
                Hour = x.AppointmentHour
            });

            serviceResponse.Data = appointments;

            return serviceResponse;
        }
        catch (Exception ex)
        {
            serviceResponse.SetFaultyState(HttpStatusCode.InternalServerError, ex);
            return serviceResponse;
        }
    }

    public async Task<ServiceResponse> GetAppointmentReasonsAsync()
    {
        var serviceResponse = new ServiceResponse();

        try
        {
            var appointmentReasonsDb = await _appointmentReasonRepository.GetAppointmentReasonsAsync();

            if (appointmentReasonsDb == null)
            {
                serviceResponse.SetFaultyState(HttpStatusCode.NotFound, Resource.AppointmentReasonsNotFoundMessage);
                return serviceResponse;
            }

            var appointmentReasons = appointmentReasonsDb.Select(x => new AppointmentReasonModel
            {
                AppointmentReasonId = x.Id,
                Name = x.Name
            });

            serviceResponse.Data = appointmentReasons;

            return serviceResponse;
        }
        catch (Exception ex)
        {
            serviceResponse.SetFaultyState(HttpStatusCode.InternalServerError, ex);
            return serviceResponse;
        }
    }

    public async Task<ServiceResponse> CreateAppointmentAsync(CreateAppointmentModel model)
    {
        var serviceResponse = new ServiceResponse();

        try
        {
            var date = model.Date;
            var hourFrom = model.Hour;
            var hourTo = model.Hour.AddMinutes(29);

            var appointments = await _appointmentRepository.GetAppointmentsByDateRangeAsync(date, hourFrom, hourTo);

            if (appointments.Any())
            {
                serviceResponse.SetFaultyState(HttpStatusCode.NotFound, Resource.AppointmentAlreadyExistsMessage);
                return serviceResponse;
            }

            var person = await _personRepository.GetPersonAsync(model.PersonId);

            if (person == null)
            {
                serviceResponse.SetFaultyState(HttpStatusCode.NotFound, Resource.PersonNotFoundMessage);
                return serviceResponse;
            }

            var appointmentReason = await _appointmentReasonRepository.GetAppointmentReasonAsync(model.AppointmentReasonId);

            if (appointmentReason == null)
            {
                serviceResponse.SetFaultyState(HttpStatusCode.NotFound, Resource.AppointmentReasonNotFoundMessage);
                return serviceResponse;
            }

            var shelter = await _shelterRepository.GetShelterAsync(model.ShelterId);

            if (shelter == null)
            {
                serviceResponse.SetFaultyState(HttpStatusCode.NotFound, Resource.ShelterNotFoundMessage);
                return serviceResponse;
            }

            var appointment = new Appointment
            {
                PersonId = model.PersonId,
                AppointmentReasonId = model.AppointmentReasonId,
                ShelterId = model.ShelterId,
                AppointmentStatusId = (int)AppointmentStatusEnum.Scheduled,
                AppointmentDate = model.Date,
                AppointmentHour = model.Hour,
            };

            await _appointmentRepository.CreateAppointmentAsync(appointment);

            var appointmentCreated = new AppointmentModel
            {
                AppointmentId = appointment.Id,
                PersonId = appointment.PersonId,
                Person = person.Name,
                AppointmentReasonId = appointment.AppointmentReasonId,
                AppointmentReason = appointmentReason.Name,
                ShelterId = appointment.ShelterId,
                Shelter = shelter.Name,
                AppointmentStatusId = appointment.AppointmentStatusId,
                AppointmentStatus = ((AppointmentStatusEnum)appointment.AppointmentStatusId).ToString(),
                Date = appointment.AppointmentDate,
                Hour = appointment.AppointmentHour
            };

            serviceResponse.Data = appointmentCreated;

            return serviceResponse;
        }
        catch (Exception ex)
        {
            serviceResponse.SetFaultyState(HttpStatusCode.InternalServerError, ex);
            return serviceResponse;
        }
    }

    public async Task<ServiceResponse> UpdateAppointmentAsync(UpdateAppointmentModel model)
    {
        var serviceResponse = new ServiceResponse();

        try
        {
            var date = model.Date;
            var hourFrom = model.Hour;
            var hourTo = model.Hour.AddMinutes(29);

            var appointment = await _appointmentRepository.GetAppointmentAsync(model.AppointmentId);

            if (appointment == null)
            {
                serviceResponse.SetFaultyState(HttpStatusCode.NotFound, Resource.AppointmentNotFoundMessage);
                return serviceResponse;
            }

            var appointments = await _appointmentRepository.GetAppointmentsByDateRangeAsync(date, hourFrom, hourTo);
            appointments = appointments.Where(x => x.Id != appointment.Id).ToList();

            if (appointments.Any())
            {
                serviceResponse.SetFaultyState(HttpStatusCode.BadRequest, Resource.AppointmentAlreadyExistsMessage);
                return serviceResponse;
            }

            var appointmentReason = await _appointmentReasonRepository.GetAppointmentReasonAsync(model.AppointmentReasonId);

            if (appointmentReason == null)
            {
                serviceResponse.SetFaultyState(HttpStatusCode.NotFound, Resource.AppointmentReasonNotFoundMessage);
                return serviceResponse;
            }

            var shelter = await _shelterRepository.GetShelterAsync(model.ShelterId);

            if (shelter == null)
            {
                serviceResponse.SetFaultyState(HttpStatusCode.NotFound, Resource.ShelterNotFoundMessage);
                return serviceResponse;
            }

            appointment.AppointmentReasonId = model.AppointmentReasonId;
            appointment.ShelterId = model.ShelterId;
            appointment.AppointmentStatusId = model.AppointmentStatusId;
            appointment.AppointmentDate = model.Date;
            appointment.AppointmentHour = model.Hour;

            await _appointmentRepository.SaveChangesAsync();

            var appointmentUpdated = new AppointmentModel
            {
                AppointmentId = appointment.Id,
                PersonId = appointment.PersonId,
                Person = appointment.Person.Name,
                AppointmentReasonId = appointment.AppointmentReasonId,
                AppointmentReason = appointmentReason.Name,
                ShelterId = appointment.ShelterId,
                Shelter = shelter.Name,
                AppointmentStatusId = appointment.AppointmentStatusId,
                AppointmentStatus = appointment.AppointmentStatus.Name,
                Date = appointment.AppointmentDate,
                Hour = appointment.AppointmentHour
            };

            serviceResponse.Data = appointmentUpdated;

            return serviceResponse;
        }
        catch (Exception ex)
        {
            serviceResponse.SetFaultyState(HttpStatusCode.InternalServerError, ex);
            return serviceResponse;
        }
    }

    public async Task<ServiceResponse> DeleteAppointmentAsync(int id)
    {
        var serviceResponse = new ServiceResponse();

        try
        {
            var appointmentDb = await _appointmentRepository.GetAppointmentAsync(id);

            if (appointmentDb == null)
            {
                serviceResponse.SetFaultyState(HttpStatusCode.BadRequest, Resource.AppointmentNotFoundMessage);
                return serviceResponse;
            }

            if (AuthPersonType == PersonTypeEnum.Client && appointmentDb.Person.Id != AuthPersonId)
            {
                serviceResponse.SetFaultyState(HttpStatusCode.BadRequest, Resource.AppointmentDeleteNotAuthorizeMessage);
                return serviceResponse;
            }

            if ((AppointmentStatusEnum)appointmentDb.AppointmentStatusId != AppointmentStatusEnum.Scheduled)
            {
                serviceResponse.SetFaultyState(HttpStatusCode.BadRequest, Resource.AppointmentStatusAlreadyChangedMessage);
                return serviceResponse;
            }

            appointmentDb.AppointmentStatusId = (int)AppointmentStatusEnum.Deleted;

            await _appointmentRepository.SaveChangesAsync();

            return serviceResponse;
        }
        catch (Exception ex)
        {
            serviceResponse.SetFaultyState(HttpStatusCode.InternalServerError, ex);
            return serviceResponse;
        }
    }
}