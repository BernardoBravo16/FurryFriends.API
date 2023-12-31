﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FurryFriends.Application.Shared.Resources {
    using System;
    
    
    /// <summary>
    ///   Clase de recurso fuertemente tipado, para buscar cadenas traducidas, etc.
    /// </summary>
    // StronglyTypedResourceBuilder generó automáticamente esta clase
    // a través de una herramienta como ResGen o Visual Studio.
    // Para agregar o quitar un miembro, edite el archivo .ResX y, a continuación, vuelva a ejecutar ResGen
    // con la opción /str o recompile su proyecto de VS.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resource {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resource() {
        }
        
        /// <summary>
        ///   Devuelve la instancia de ResourceManager almacenada en caché utilizada por esta clase.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("FurryFriends.Application.Shared.Resources.Resource", typeof(Resource).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Reemplaza la propiedad CurrentUICulture del subproceso actual para todas las
        ///   búsquedas de recursos mediante esta clase de recurso fuertemente tipado.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Ya existe un registro de adopción con el animal y la persona ingresadas..
        /// </summary>
        internal static string AdoptionAlreadyExistsMessage {
            get {
                return ResourceManager.GetString("AdoptionAlreadyExistsMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a No se encontró la adopción ingresada..
        /// </summary>
        internal static string AdoptionNotFoundMessage {
            get {
                return ResourceManager.GetString("AdoptionNotFoundMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a No se encontraron adopciones asociadas al animal ingresado..
        /// </summary>
        internal static string AdoptionsByAnimalIdNotFoundMessage {
            get {
                return ResourceManager.GetString("AdoptionsByAnimalIdNotFoundMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a No se encontraron adopciones asociadas a la persona ingresada..
        /// </summary>
        internal static string AdoptionsByPersonIdNotFoundMessage {
            get {
                return ResourceManager.GetString("AdoptionsByPersonIdNotFoundMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Ya existe un animal con el número de identificación ingresado..
        /// </summary>
        internal static string AnimalAlreadyExistsMessage {
            get {
                return ResourceManager.GetString("AnimalAlreadyExistsMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a No se encontró al animal ingresado..
        /// </summary>
        internal static string AnimalNotFoundMessage {
            get {
                return ResourceManager.GetString("AnimalNotFoundMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Ya existe una cita en la fecha y hora ingresadas..
        /// </summary>
        internal static string AppointmentAlreadyExistsMessage {
            get {
                return ResourceManager.GetString("AppointmentAlreadyExistsMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a El usuario no tiene los permisos requeridos para eliminar la cita..
        /// </summary>
        internal static string AppointmentDeleteNotAuthorizeMessage {
            get {
                return ResourceManager.GetString("AppointmentDeleteNotAuthorizeMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a No se encontró la cita..
        /// </summary>
        internal static string AppointmentNotFoundMessage {
            get {
                return ResourceManager.GetString("AppointmentNotFoundMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a No se encontró el motivo de visita ingresado..
        /// </summary>
        internal static string AppointmentReasonNotFoundMessage {
            get {
                return ResourceManager.GetString("AppointmentReasonNotFoundMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a No se encontraron motivos de visita..
        /// </summary>
        internal static string AppointmentReasonsNotFoundMessage {
            get {
                return ResourceManager.GetString("AppointmentReasonsNotFoundMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a No se encontraron citas..
        /// </summary>
        internal static string AppointmentsNotFoundMessage {
            get {
                return ResourceManager.GetString("AppointmentsNotFoundMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Solo se pueden editar citas que se encuentren en estado agendado..
        /// </summary>
        internal static string AppointmentStatusAlreadyChangedMessage {
            get {
                return ResourceManager.GetString("AppointmentStatusAlreadyChangedMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a No se encontró la raza de animal ingresada..
        /// </summary>
        internal static string BreedNotFoundMessage {
            get {
                return ResourceManager.GetString("BreedNotFoundMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a El usuario está deshabilitado, no es posible iniciar sesión..
        /// </summary>
        internal static string DisableUserMessage {
            get {
                return ResourceManager.GetString("DisableUserMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a El rol del usuario se encuentra inactivo, no es posible iniciar sesión..
        /// </summary>
        internal static string DisableUserRoleMessage {
            get {
                return ResourceManager.GetString("DisableUserRoleMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Ya existe una cuenta con el correo electrónico ingresado..
        /// </summary>
        internal static string EmailAlreadyExistsMessage {
            get {
                return ResourceManager.GetString("EmailAlreadyExistsMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Nombre de usuario o contraseña incorrectos..
        /// </summary>
        internal static string InvalidCredentialsMessage {
            get {
                return ResourceManager.GetString("InvalidCredentialsMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a La nueva contraseña y la confirmación no coinciden..
        /// </summary>
        internal static string InvalidNewPasswordConfirmedMessage {
            get {
                return ResourceManager.GetString("InvalidNewPasswordConfirmedMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a La ubicación del animal en el rango de fechas ingresado ya fue registrada con anterioridad..
        /// </summary>
        internal static string LocationAlreadyExistsMessage {
            get {
                return ResourceManager.GetString("LocationAlreadyExistsMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a No se encontró la ubicación ingresada..
        /// </summary>
        internal static string LocationNotFoundMessage {
            get {
                return ResourceManager.GetString("LocationNotFoundMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a No se encontraron ubicaciones de animales con el animal ingresado..
        /// </summary>
        internal static string LocationsByAnimalIdNotFoundMessage {
            get {
                return ResourceManager.GetString("LocationsByAnimalIdNotFoundMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a No se encontraron ubicaciones de animales con el refugio ingresado..
        /// </summary>
        internal static string LocationsByShelterIdNotFoundMessage {
            get {
                return ResourceManager.GetString("LocationsByShelterIdNotFoundMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a No se encontró el registro médico ingresado..
        /// </summary>
        internal static string MedicalRecordNotFoundMessage {
            get {
                return ResourceManager.GetString("MedicalRecordNotFoundMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a No se encontraron registros médicos para el animal ingresado..
        /// </summary>
        internal static string MedicalRecordsByAnimalIdNotFoundMessage {
            get {
                return ResourceManager.GetString("MedicalRecordsByAnimalIdNotFoundMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a No se encontraron registros médicos con el veterinario ingresado..
        /// </summary>
        internal static string MedicalRecordsByVeterinaryIdNotFoundMessage {
            get {
                return ResourceManager.GetString("MedicalRecordsByVeterinaryIdNotFoundMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Ya existe una persona con la indentificación ingresada..
        /// </summary>
        internal static string PersonAlreadyExistsMessage {
            get {
                return ResourceManager.GetString("PersonAlreadyExistsMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Ya existe una persona utilizando el correo electrónico ingresado..
        /// </summary>
        internal static string PersonEmailAlreadyExistsMessage {
            get {
                return ResourceManager.GetString("PersonEmailAlreadyExistsMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Ya hay un usuario asociado a la persona con la identificación ingresada..
        /// </summary>
        internal static string PersonIsAlreadyAssociatedWithUserMessage {
            get {
                return ResourceManager.GetString("PersonIsAlreadyAssociatedWithUserMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a No se encontró a la persona ingresada..
        /// </summary>
        internal static string PersonNotFoundMessage {
            get {
                return ResourceManager.GetString("PersonNotFoundMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Ya existe un refugio con el nombre ingresado..
        /// </summary>
        internal static string ShelterAlreadyExistsMessage {
            get {
                return ResourceManager.GetString("ShelterAlreadyExistsMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Ya existe un refugio con el correo electrónico ingresado..
        /// </summary>
        internal static string ShelterEmailAlreadyExistsMessage {
            get {
                return ResourceManager.GetString("ShelterEmailAlreadyExistsMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a No se encontró el refugio ingresado..
        /// </summary>
        internal static string ShelterNotFoundMessage {
            get {
                return ResourceManager.GetString("ShelterNotFoundMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a No se encontraron refugios..
        /// </summary>
        internal static string SheltersNotFoundMessage {
            get {
                return ResourceManager.GetString("SheltersNotFoundMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Ya existe una cuenta con el nombre de usuario ingresado..
        /// </summary>
        internal static string UserAlreadyExistsMessage {
            get {
                return ResourceManager.GetString("UserAlreadyExistsMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a No se encontró el usuario ingresado..
        /// </summary>
        internal static string UserNotFoundMessage {
            get {
                return ResourceManager.GetString("UserNotFoundMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a No se encontraron usuarios..
        /// </summary>
        internal static string UsersNotFoundMessage {
            get {
                return ResourceManager.GetString("UsersNotFoundMessage", resourceCulture);
            }
        }
    }
}
