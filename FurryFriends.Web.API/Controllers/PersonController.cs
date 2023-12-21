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
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;
        private readonly IWebHostEnvironment _environment;

        public PersonController(IPersonService personService, IWebHostEnvironment webHostEnvironment)
        {
            _personService = personService;
            _environment = webHostEnvironment;
        }

        [HttpGet("get-person/{id}")]
        public async Task<IActionResult> GetPerson(int id)
        {
            var presenter = new BasePresenter();
            return presenter.Execute(new ApiResponse(await _personService.GetPersonAsync(id)));
        }

        [HttpGet("get-persons")]
        public async Task<IActionResult> GetPersons()
        {
            var presenter = new BasePresenter();
            return presenter.Execute(new ApiResponse(await _personService.GetPersonsAsync()));
        }

        [HttpGet("get-persons-not-users")]
        public async Task<IActionResult> GetPersonsNotUsers()
        {
            var presenter = new BasePresenter();
            return presenter.Execute(new ApiResponse(await _personService.GetPersonsNotUsersAsync()));
        }

        [HttpGet("get-persons-by-person-role/{role}")]
        public async Task<IActionResult> GetPersonsByPersonRole(string role)
        {
            var presenter = new BasePresenter();
            return presenter.Execute(new ApiResponse(await _personService.GetPersonsByPersonRoleAsync(role)));
        }

        [HttpPost("create-person")]
        public async Task<IActionResult> CreatePerson(CreatePersonModel model)
        {
            var presenter = new BasePresenter();
            return presenter.Execute(new ApiResponse(await _personService.CreatePersonAsync(model)));
        }

        [HttpPost("update-person")]
        public async Task<IActionResult> UpdatePerson(UpdatePersonModel model)
        {
            var presenter = new BasePresenter();
            return presenter.Execute(new ApiResponse(await _personService.UpdatePersonAsync(model)));
        }

        [HttpPost("update-photo/{personId}")]
        public async Task<IActionResult> UpdatePhoto(int personId, [FromForm] IFormFile file)
        {
            var newFileName = string.Empty;
            var fileName = string.Empty;
            string PathDB = string.Empty;

            if (file.Length > 0)
            {
                //Getting FileName
                fileName = file.FileName;
                //Assigning Unique Filename (Guid)
                var myUniqueFileName = Convert.ToString(Guid.NewGuid());
                //Getting file Extension
                var FileExtension = Path.GetExtension(fileName);
                // concating  FileName + FileExtension
                newFileName = myUniqueFileName + FileExtension;
                // Combines two strings into a path.
                fileName = Path.Combine(_environment.WebRootPath, "Pictures") + $@"\{newFileName}";
                // if you want to store path of folder in database
                PathDB = "demoImages/" + newFileName;
                using (FileStream fs = System.IO.File.Create(fileName))
                {
                    file.CopyTo(fs);
                    fs.Flush();
                }
            }

            return Ok();
        }
    }
}
