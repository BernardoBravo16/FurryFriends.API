using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Reflection;
using FurryFriends.Application.Shared.Contracts.Security;
using FurryFriends.Domain.Enum;

namespace FurryFriends.Web.API.Shared.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class AuthenticatedUserContextAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        base.OnActionExecuting(context);

        var allowAnonymousAttribute = (context.ActionDescriptor as ControllerActionDescriptor)
            .MethodInfo
            .GetCustomAttributes<AllowAnonymousAttribute>()
            .FirstOrDefault();

        if (allowAnonymousAttribute != null)
            return;

        var controller = (ControllerBase)context.Controller;

        var userClaim = context.HttpContext.User;

        var authenticatedUserData = new
        {
            userId = userClaim.FindFirst("UserId").Value,
            Username = userClaim.FindFirst("Username").Value,
            Email = userClaim.FindFirst("Email").Value,
            RoleId = userClaim.FindFirst("RoleId").Value,
            PersonId = userClaim.FindFirst("PersonId").Value,
            PersonTypeId = userClaim.FindFirst("PersonTypeId").Value
        };

        if (string.IsNullOrEmpty(authenticatedUserData.userId))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        var fields = context
            .Controller
            .GetType()
            .GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
            .Where(f => typeof(IAuthenticatedUserContext).IsAssignableFrom(f.FieldType))
            .ToArray();

        foreach (var field in fields)
        {
            var userContextField = (IAuthenticatedUserContext)field.GetValue(context.Controller);

            userContextField.AuthUserId = Convert.ToInt32(authenticatedUserData.userId);
            userContextField.AuthUsername = authenticatedUserData.Username;
            userContextField.AuthEmail = authenticatedUserData.Email;
            userContextField.AuthPersonId = Convert.ToInt32(authenticatedUserData.PersonId);
            userContextField.AuthRole = (RoleEnum)Convert.ToInt32(authenticatedUserData.RoleId);
            userContextField.AuthPersonType = (PersonTypeEnum)Convert.ToInt32(authenticatedUserData.PersonTypeId);
        }
    }
}
