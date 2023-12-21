using FurryFriends.Application.Services.Interfaces;
using FurryFriends.Application.Services.Models;
using FurryFriends.Web.API.Shared;
using FurryFriends.Web.API.Shared.Attributes;
using FurryFriends.Web.API.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FurryFriends.Web.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
[AuthenticatedUserContext]
public class UserController : ControllerBase
{
    private IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("get-user/{id}")]
    public async Task<IActionResult> GetUser(int id)
    {
        var presenter = new BasePresenter();
        return presenter.Execute(new ApiResponse(await _userService.GetUserAsync(id)));
    }

    [HttpGet("get-users")]
    public async Task<IActionResult> GetUsers()
    {
        var presenter = new BasePresenter();
        return presenter.Execute(new ApiResponse(await _userService.GetUsersAsync()));
    }

    [HttpPost("create-user")]
    public async Task<IActionResult> CreateUser(CreateUserModel model)
    {
        var presenter = new BasePresenter();
        return presenter.Execute(new ApiResponse(await _userService.CreateUserAsync(model)));
    }

    [HttpPost("create-only-user")]
    public async Task<IActionResult> CreateUserOnly(CreateUserOnlyModel model)
    {
        var presenter = new BasePresenter();
        return presenter.Execute(new ApiResponse(await _userService.CreateUserOnlyAsync(model)));
    }

    [HttpPost("update-user")]
    public async Task<IActionResult> UpdateUser(UpdateUserModel model)
    {
        var presenter = new BasePresenter();
        return presenter.Execute(new ApiResponse(await _userService.UpdateUserAsync(model)));
    }

    [HttpPost("update-only-user")]
    public async Task<IActionResult> UpdateUserOnly(UpdateUserOnlyModel model)
    {
        var presenter = new BasePresenter();
        return presenter.Execute(new ApiResponse(await _userService.UpdateUserOnlyAsync(model)));
    }
}