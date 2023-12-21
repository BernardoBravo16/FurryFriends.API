using FurryFriends.Web.API.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FurryFriends.Web.API.Shared;

public class BasePresenter : Controller
{
    public IActionResult Execute(ApiResponse apiResponse)
    {
        if (!apiResponse.Status)
        {
            switch (apiResponse.StatusCode)
            {
                case HttpStatusCode.BadRequest:
                    return SetBadRequestObject(apiResponse.Errors);
                case HttpStatusCode.Unauthorized:
                    return SetUnauthorizedObject(apiResponse.Errors);
                case HttpStatusCode.NotFound:
                    return SetNotFoundObject(apiResponse.Errors);
                default:
                    return StatusCode((int)apiResponse.StatusCode, apiResponse.Errors);
            }
        }

        if (apiResponse.Data is null)
            return SetOk();
        else
            return SetOkObject(apiResponse.Data);
    }

    public IActionResult SetOk()
    {
        return new OkResult();
    }

    public IActionResult SetOkObject(object @object)
    {
        return new OkObjectResult(@object);
    }

    public IActionResult SetBadRequestObject(object @object)
    {
        return new BadRequestObjectResult(@object);
    }

    public IActionResult SetUnauthorizedObject(object @object)
    {
        return new UnauthorizedObjectResult(@object);
    }

    public IActionResult SetNotFoundObject(object @object)
    {
        return new NotFoundObjectResult(@object);
    }
}
