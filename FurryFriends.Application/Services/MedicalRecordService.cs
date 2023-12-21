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

public class MedicalRecordService : BaseService, IMedicalRecordService
{
    private readonly IMedicalRecordRepository _medicalRecordRepository;
    private readonly IAnimalRepository _animalRepository;
    private readonly IPersonRepository _personRepository;

    public MedicalRecordService(IMedicalRecordRepository medicalRecordRepository,
        IAnimalRepository animalRepository,
        IPersonRepository personRepository)
    {
        _medicalRecordRepository = medicalRecordRepository;
        _animalRepository = animalRepository;
        _personRepository = personRepository;
    }

    public async Task<ServiceResponse> GetMedicalRecordAsync(int id)
    {
        var serviceResponse = new ServiceResponse();

        try
        {
            var medicalRecordDb = await _medicalRecordRepository.GetMedicalRecordAsync(id);

            if (medicalRecordDb == null)
            {
                serviceResponse.SetFaultyState(HttpStatusCode.NotFound, Resource.MedicalRecordNotFoundMessage);
                return serviceResponse;
            }

            var medicalRecord = new MedicalRecordModel
            {
                MedicalRecordId = medicalRecordDb.Id,
                AnimalId = medicalRecordDb.AnimalId,
                Animal = medicalRecordDb.Animal.Name,
                VeterinaryId = medicalRecordDb.VeterinaryId,
                Veterinary = medicalRecordDb.Person.Name,
                Treatment_Date = medicalRecordDb.Treatment_Date,
                Diagnosis = medicalRecordDb.Diagnosis,
                Treatment = medicalRecordDb.Treatment
            };

            serviceResponse.Data = medicalRecord;

            return serviceResponse;
        }
        catch (Exception ex)
        {
            serviceResponse.SetFaultyState(HttpStatusCode.InternalServerError, ex);
            return serviceResponse;
        }
    }

    public async Task<ServiceResponse> GetMedicalRecordsByAnimalIdAsync(int animalId)
    {
        var serviceResponse = new ServiceResponse();

        try
        {
            var medicalRecordsDb = await _medicalRecordRepository.GetMedicalRecordsByAnimalIdAsync(animalId);

            if (medicalRecordsDb == null)
            {
                serviceResponse.SetFaultyState(HttpStatusCode.NotFound, Resource.MedicalRecordsByAnimalIdNotFoundMessage);
                return serviceResponse;
            }

            var medicalRecords = medicalRecordsDb.Select(x => new MedicalRecordModel
            {
                MedicalRecordId = x.Id,
                AnimalId = x.AnimalId,
                Animal = x.Animal.Name,
                VeterinaryId = x.VeterinaryId,
                Veterinary = x.Person.Name,
                Treatment_Date = x.Treatment_Date,
                Diagnosis = x.Diagnosis,
                Treatment = x.Treatment
            });

            serviceResponse.Data = medicalRecords;

            return serviceResponse;
        }
        catch (Exception ex)
        {
            serviceResponse.SetFaultyState(HttpStatusCode.InternalServerError, ex);
            return serviceResponse;
        }
    }

    public async Task<ServiceResponse> GetMedicalRecordsByVeterinaryIdAsync(int veterinaryId)
    {
        var serviceResponse = new ServiceResponse();

        try
        {
            var medicalRecordsDb = await _medicalRecordRepository.GetMedicalRecordsByPersonIdAsync(veterinaryId);

            if (medicalRecordsDb == null)
            {
                serviceResponse.SetFaultyState(HttpStatusCode.NotFound, Resource.MedicalRecordsByVeterinaryIdNotFoundMessage);
                return serviceResponse;
            }

            var medicalRecords = medicalRecordsDb.Select(x => new MedicalRecordModel
            {
                MedicalRecordId = x.Id,
                AnimalId = x.AnimalId,
                Animal = x.Animal.Name,
                VeterinaryId = x.VeterinaryId,
                Veterinary = x.Person.Name,
                Treatment_Date = x.Treatment_Date,
                Diagnosis = x.Diagnosis,
                Treatment = x.Treatment
            });

            serviceResponse.Data = medicalRecords;

            return serviceResponse;
        }
        catch (Exception ex)
        {
            serviceResponse.SetFaultyState(HttpStatusCode.InternalServerError, ex);
            return serviceResponse;
        }
    }

    public async Task<ServiceResponse> CreateMedicalRecordAsync(CreateMedicalRecordModel model)
    {
        var serviceResponse = new ServiceResponse();

        try
        {
            var animal = await _animalRepository.GetAnimalAsync(model.AnimalId);

            if (animal == null)
            {
                serviceResponse.SetFaultyState(HttpStatusCode.NotFound, Resource.AnimalNotFoundMessage);
                return serviceResponse;
            }

            var veterinary = await _personRepository.GetPersonAsync(model.VeterinaryId);

            if (veterinary == null)
            {
                serviceResponse.SetFaultyState(HttpStatusCode.NotFound, Resource.PersonNotFoundMessage);
                return serviceResponse;
            }

            var medicalRecord = new MedicalRecord
            {
                AnimalId = model.AnimalId,
                VeterinaryId = veterinary.Id,
                Treatment_Date = model.Treatment_Date,
                Diagnosis = model.Diagnosis,
                Treatment = model.Treatment,
            };

            await _medicalRecordRepository.CreateMedicalRecordAsync(medicalRecord);

            var medicalRecordCreated = new MedicalRecordModel
            {
                MedicalRecordId = medicalRecord.Id,
                AnimalId = medicalRecord.AnimalId,
                Animal = medicalRecord.Animal.Name,
                VeterinaryId = medicalRecord.VeterinaryId,
                Veterinary = veterinary.Name,
                Treatment_Date = medicalRecord.Treatment_Date,
                Diagnosis = medicalRecord.Diagnosis,
                Treatment = medicalRecord.Treatment
            };

            serviceResponse.Data = medicalRecordCreated;

            return serviceResponse;
        }
        catch (Exception ex)
        {
            serviceResponse.SetFaultyState(HttpStatusCode.InternalServerError, ex);
            return serviceResponse;
        }
    }
}