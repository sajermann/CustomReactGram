using Application.Dtos;

namespace Application.Interfaces
{
  public interface IPhotoService
  {
    Task<PhotoDtoOut> CreatePhoto(PhotoDtoIn photo, string jwt);
  }
}
