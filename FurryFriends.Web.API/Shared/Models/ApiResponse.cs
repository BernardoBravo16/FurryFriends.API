using FurryFriends.Application.Shared.Models.Base;
using System.Net;

namespace FurryFriends.Web.API.Shared.Models;

public class ApiResponse : ApiResponse<object>
{
    public ApiResponse(ServiceResponse serviceResponse) : base(serviceResponse) { }
}

public class ApiResponse<T>
{
    public T Data { get; set; }
    public bool Status => !Errors.Any();
    public HttpStatusCode StatusCode { get; set; }

    public ICollection<string> Errors { get; set; }

    public ApiResponse()
    {
        Errors = new List<string>();
    }

    public ApiResponse(ServiceResponse<T> serviceResponse)
    {
        Data = serviceResponse.Data;
        StatusCode = serviceResponse.StatusCode;
        Errors = serviceResponse.Errors
            .Select(e => e.ErrorMessage)
            .ToList();
    }

    public ApiResponse(ServiceResponse serviceResponse)
    {
        Data = (T)serviceResponse.Data;
        StatusCode = serviceResponse.StatusCode;
        Errors = serviceResponse.Errors
            .Select(e => e.ErrorMessage)
            .ToList();
    }
}
