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
public class AdoptionController : ControllerBase
{
    private readonly IAdoptionService _adoptionService;

    public AdoptionController(IAdoptionService adoptionService)
    {
        _adoptionService = adoptionService;
    }

    [HttpGet("get-adoption/{id}")]
    public async Task<IActionResult> GetAdoption(int id)
    {
        var presenter = new BasePresenter();
        return presenter.Execute(new ApiResponse(await _adoptionService.GetAdoptionAsync(id)));
    }

    [HttpGet("get-adoptions-by-animal-id/{id}")]
    public async Task<IActionResult> GetAdoptionsByAnimalIdAsync(int id)
    {
        var presenter = new BasePresenter();
        return presenter.Execute(new ApiResponse(await _adoptionService.GetAdoptionsByAnimalIdAsync(id)));
    }

    [HttpGet("get-adoptions-by-person-id/{id}")]
    public async Task<IActionResult> GetAdoptionsByPersonIdAsync(int id)
    {
        var presenter = new BasePresenter();
        return presenter.Execute(new ApiResponse(await _adoptionService.GetAdoptionsByPersonIdAsync(id)));
    }

    [HttpPost("create-adoption")]
    public async Task<IActionResult> CreateAdoption(CreateAdoptionModel model)
    {
        var presenter = new BasePresenter();
        return presenter.Execute(new ApiResponse(await _adoptionService.CreateAdoptionAsync(model)));
    }
}