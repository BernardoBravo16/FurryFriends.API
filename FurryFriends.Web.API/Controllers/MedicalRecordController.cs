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
public class MedicalRecordController : ControllerBase
{
    private readonly IMedicalRecordService _medicalRecordService;

    public MedicalRecordController(IMedicalRecordService medicalRecordService)
    {
        _medicalRecordService = medicalRecordService;
    }

    [HttpGet("get-medicalRecord/{id}")]
    public async Task<IActionResult> GetMedicalRecord(int id)
    {
        var presenter = new BasePresenter();
        return presenter.Execute(new ApiResponse(await _medicalRecordService.GetMedicalRecordAsync(id)));
    }

    [HttpGet("get-medicalRecords-by-animal-id/{id}")]
    public async Task<IActionResult> GetMedicalRecordsByAnimalIdAsync(int id)
    {
        var presenter = new BasePresenter();
        return presenter.Execute(new ApiResponse(await _medicalRecordService.GetMedicalRecordsByAnimalIdAsync(id)));
    }

    [HttpGet("get-medicalRecords-by-veterinary-id/{id}")]
    public async Task<IActionResult> GetMedicalRecordsByShelterIdAsync(int id)
    {
        var presenter = new BasePresenter();
        return presenter.Execute(new ApiResponse(await _medicalRecordService.GetMedicalRecordsByVeterinaryIdAsync(id)));
    }

    [HttpPost("create-medicalRecord")]
    public async Task<IActionResult> CreateMedicalRecord(CreateMedicalRecordModel model)
    {
        var presenter = new BasePresenter();
        return presenter.Execute(new ApiResponse(await _medicalRecordService.CreateMedicalRecordAsync(model)));
    }
}