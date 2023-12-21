using FurryFriends.Web.API.Shared.Models;
using FurryFriends.Web.API.Shared;
using Microsoft.AspNetCore.Mvc;
using FurryFriends.Application.Services.Interfaces;
using FurryFriends.Application.Services.Models;
using FurryFriends.Web.API.Shared.Attributes;
using Microsoft.AspNetCore.Authorization;

namespace FurryFriends.Web.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
[AuthenticatedUserContext]
public class LocationController : ControllerBase
{
    private readonly ILocationService _locationService;

    public LocationController(ILocationService locationService)
    {
        _locationService = locationService;
    }

    [HttpGet("get-location/{id}")]
    public async Task<IActionResult> GetLocation(int id)
    {
        var presenter = new BasePresenter();
        return presenter.Execute(new ApiResponse(await _locationService.GetLocationAsync(id)));
    }

    [HttpGet("get-locations-by-animal-id/{id}")]
    public async Task<IActionResult> GetLocationsByAnimalIdAsync(int id)
    {
        var presenter = new BasePresenter();
        return presenter.Execute(new ApiResponse(await _locationService.GetLocationsByAnimalIdAsync(id)));
    }

    [HttpGet("get-locations-by-shelter-id/{id}")]
    public async Task<IActionResult> GetLocationsByShelterIdAsync(int id)
    {
        var presenter = new BasePresenter();
        return presenter.Execute(new ApiResponse(await _locationService.GetLocationsByShelterIdAsync(id)));
    }

    [HttpPost("create-location")]
    public async Task<IActionResult> CreateLocation(CreateLocationModel model)
    {
        var presenter = new BasePresenter();
        return presenter.Execute(new ApiResponse(await _locationService.CreateLocationAsync(model)));
    }
}