using FurryFriends.Web.API.Shared.Models;
using FurryFriends.Web.API.Shared;
using Microsoft.AspNetCore.Mvc;
using FurryFriends.Application.Services.Authentication.Interfaces;
using FurryFriends.Application.Services.Authentication.Models;

namespace FurryFriends.Web.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticateUserUseCase)
    {
        _authenticationService = authenticateUserUseCase;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(CredentialsModel model)
    {
        var presenter = new BasePresenter();
        return presenter.Execute(new ApiResponse(await _authenticationService.GenerateAuthenticationTokenAsync(model)));
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserModel model)
    {
        var presenter = new BasePresenter();
        return presenter.Execute(new ApiResponse(await _authenticationService.RegisterUserAsync(model)));
    }

    [HttpPost("change-password")]
    public async Task<IActionResult> ChangePassword(ChangePasswordModel model)
    {
        var presenter = new BasePresenter();
        return presenter.Execute(new ApiResponse(await _authenticationService.ChangeUserPasswordAsync(model)));
    }
}

