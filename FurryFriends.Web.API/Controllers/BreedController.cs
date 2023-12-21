using FurryFriends.Application.Services.Interfaces;
using FurryFriends.Web.API.Shared;
using FurryFriends.Web.API.Shared.Attributes;
using FurryFriends.Web.API.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FurryFriends.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [AuthenticatedUserContext]
    public class BreedController : ControllerBase
    {
        private readonly IBreedService _breedService;

        public BreedController(IBreedService breedService)
        {
            _breedService = breedService;
        }

        [HttpGet("get-breed/{id}")]
        public async Task<IActionResult> GetBreed(int id)
        {
            var presenter = new BasePresenter();
            return presenter.Execute(new ApiResponse(await _breedService.GetBreedAsync(id)));
        }

        [HttpGet("get-breeds")]
        public async Task<IActionResult> GetBreeds()
        {
            var presenter = new BasePresenter();
            return presenter.Execute(new ApiResponse(await _breedService.GetBreedsAsync()));
        }
    }
}
