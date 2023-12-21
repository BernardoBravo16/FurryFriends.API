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
public class AppointmentController : ControllerBase
{
    private readonly IAppointmentService _appointmentService;

    public AppointmentController(IAppointmentService appointmentService)
    {
        _appointmentService = appointmentService;
    }

    [HttpGet("get-appointment/{id}")]
    public async Task<IActionResult> GetAppointment(int id)
    {
        var presenter = new BasePresenter();
        return presenter.Execute(new ApiResponse(await _appointmentService.GetAppointmentAsync(id)));
    }

    [HttpGet("get-appointments")]
    public async Task<IActionResult> GetAppointments()
    {
        var presenter = new BasePresenter();
        return presenter.Execute(new ApiResponse(await _appointmentService.GetAppointmentsAsync()));
    }

    [HttpGet("get-appointment-reasons")]
    public async Task<IActionResult> GetAppointmentReasons()
    {
        var presenter = new BasePresenter();
        return presenter.Execute(new ApiResponse(await _appointmentService.GetAppointmentReasonsAsync()));
    }

    [HttpPost("create-appointment")]
    public async Task<IActionResult> CreateAppointment(CreateAppointmentModel model)
    {
        var presenter = new BasePresenter();
        return presenter.Execute(new ApiResponse(await _appointmentService.CreateAppointmentAsync(model)));
    }

    [HttpPost("update-appointment")]
    public async Task<IActionResult> UpdateAppointment(UpdateAppointmentModel model)
    {
        var presenter = new BasePresenter();
        return presenter.Execute(new ApiResponse(await _appointmentService.UpdateAppointmentAsync(model)));
    }

    [HttpDelete("delete-appointment/{id}")]
    public async Task<IActionResult> DeleteAppointment(int id)
    {
        var presenter = new BasePresenter();
        return presenter.Execute(new ApiResponse(await _appointmentService.DeleteAppointmentAsync(id)));
    }
}