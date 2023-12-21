using FurryFriends.Application.Shared.Contracts.Persistence;
using FurryFriends.Application.Shared.Contracts.Persistence.Repositories;
using FurryFriends.Persistence;
using FurryFriends.Persistence.Repositories;

namespace FurryFriends.Web.API.Shared.Initialize;

public static class PersistenceDependenciesManager
{
    public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.Add(new ServiceDescriptor(typeof(IDatabaseContext), typeof(DatabaseContext), ServiceLifetime.Scoped));
        services.Add(new ServiceDescriptor(typeof(IUnitOfWork), typeof(UnitOfWork), ServiceLifetime.Transient));
        services.Add(new ServiceDescriptor(typeof(IRepository<,>), typeof(Repository<,>), ServiceLifetime.Transient));
        services.Add(new ServiceDescriptor(typeof(IUserRepository), typeof(UserRepository), ServiceLifetime.Transient));
        services.Add(new ServiceDescriptor(typeof(IContactRepository), typeof(ContactRepository), ServiceLifetime.Transient));
        services.Add(new ServiceDescriptor(typeof(IPersonRepository), typeof(PersonRepository), ServiceLifetime.Transient));
        services.Add(new ServiceDescriptor(typeof(IShelterRepository), typeof(ShelterRepository), ServiceLifetime.Transient));
        services.Add(new ServiceDescriptor(typeof(IAppointmentRepository), typeof(AppointmentRepository), ServiceLifetime.Transient));
        services.Add(new ServiceDescriptor(typeof(IAppointmentReasonRepository), typeof(AppointmentReasonRepository), ServiceLifetime.Transient));
        services.Add(new ServiceDescriptor(typeof(IAnimalRepository), typeof(AnimalRepository), ServiceLifetime.Transient));
        services.Add(new ServiceDescriptor(typeof(IBreedRepository), typeof(BreedRepository), ServiceLifetime.Transient));
        services.Add(new ServiceDescriptor(typeof(ILocationRepository), typeof(LocationRepository), ServiceLifetime.Transient));
        services.Add(new ServiceDescriptor(typeof(IMedicalRecordRepository), typeof(MedicalRecordRepository), ServiceLifetime.Transient));
        services.Add(new ServiceDescriptor(typeof(IAdoptionRepository), typeof(AdoptionRepository), ServiceLifetime.Transient));
    }
}