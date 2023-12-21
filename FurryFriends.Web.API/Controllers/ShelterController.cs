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
public class ShelterController : ControllerBase
{
    private readonly IShelterService _shelterService;

    public ShelterController(IShelterService shelterService)
    {
        _shelterService = shelterService;
    }

    [HttpGet("get-shelter/{id}")]
    public async Task<IActionResult> GetShelter(int id)
    {
        var presenter = new BasePresenter();
        return presenter.Execute(new ApiResponse(await _shelterService.GetShelterAsync(id)));
    }

    [HttpGet("get-shelters")]
    public async Task<IActionResult> GetShelters()
    {
        var presenter = new BasePresenter();
        return presenter.Execute(new ApiResponse(await _shelterService.GetSheltersAsync()));
    }

    [HttpPost("create-shelter")]
    public async Task<IActionResult> CreateShelter(CreateShelterModel model)
    {
        var presenter = new BasePresenter();
        return presenter.Execute(new ApiResponse(await _shelterService.CreateShelterAsync(model)));
    }

    [HttpPost("update-shelter")]
    public async Task<IActionResult> UpdateShelter(UpdateShelterModel model)
    {
        var presenter = new BasePresenter();
        return presenter.Execute(new ApiResponse(await _shelterService.UpdateShelterAsync(model)));
    }
}