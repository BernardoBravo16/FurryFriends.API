using FurryFriends.Application.Services.Interfaces;
using FurryFriends.Application.Services.Models;
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
    public class AnimalController : ControllerBase
    {
        private readonly IAnimalService _animalService;

        public AnimalController(IAnimalService animalService)
        {
            _animalService = animalService;
        }

        [HttpGet("get-animal/{id}")]
        public async Task<IActionResult> GetAnimal(int id)
        {
            var presenter = new BasePresenter();
            return presenter.Execute(new ApiResponse(await _animalService.GetAnimalAsync(id)));
        }

        [HttpGet("get-animals")]
        public async Task<IActionResult> GetAnimals()
        {
            var presenter = new BasePresenter();
            return presenter.Execute(new ApiResponse(await _animalService.GetAnimalsAsync()));
        }

        [HttpPost("create-animal")]
        public async Task<IActionResult> CreateAnimal(CreateAnimalModel model)
        {
            var presenter = new BasePresenter();
            return presenter.Execute(new ApiResponse(await _animalService.CreateAnimalAsync(model)));
        }

        [HttpPost("update-animal")]
        public async Task<IActionResult> UpdateAnimal(UpdateAnimalModel model)
        {
            var presenter = new BasePresenter();
            return presenter.Execute(new ApiResponse(await _animalService.UpdateAnimalAsync(model)));
        }
    }
}
