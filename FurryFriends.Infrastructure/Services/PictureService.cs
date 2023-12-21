using AnimalShelter.Application.Shared;
using AnimalShelter.Application.Shared.Contracts.Infrastructure;
using AnimalShelter.Infrastructure.Services.Models;

namespace AnimalShelter.Infrastructure.Services;

public class PictureService : BaseService, IPictureService
{
    public async Task<PictureSavedModel> SavePicture()
    {
        return null;
    }
}