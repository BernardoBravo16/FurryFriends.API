using FurryFriends.Application.Services;
using FurryFriends.Application.Services.Authentication;
using FurryFriends.Application.Services.Authentication.Interfaces;
using FurryFriends.Application.Services.Interfaces;

namespace FurryFriends.Web.API.Shared.Initialize;

public static class ApplicationDependenciesManager
{
    public static void AddApplication(this IServiceCollection services)
    {
        //Security
        services.AddTransient(typeof(ITokenService), typeof(TokenService));
        services.AddTransient(typeof(IEncryptionService), typeof(EncryptionService));

        //Authentication
        services.AddTransient(typeof(IAuthenticationService), typeof(AuthenticationService));

        //User
        services.AddTransient(typeof(IUserService), typeof(UserService));

        //Person
        services.AddTransient(typeof(IPersonService), typeof(PersonService));

        //Shelter
        services.AddTransient(typeof(IShelterService), typeof(ShelterService));

        //Appointment
        services.AddTransient(typeof(IAppointmentService), typeof(AppointmentService));

        //Animal
        services.AddTransient(typeof(IAnimalService), typeof(AnimalService));
        services.AddTransient(typeof(IBreedService), typeof(BreedService));
        services.AddTransient(typeof(ILocationService), typeof(LocationService));
        services.AddTransient(typeof(IMedicalRecordService), typeof(MedicalRecordService));

        //Adoption
        services.AddTransient(typeof(IAdoptionService), typeof(AdoptionService));
    }
}