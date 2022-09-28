using Application.Dtos;

namespace Application.Interfaces
{
  public interface IPhotoService
  {
    Task<IList<PhotoDtoOut>> GetAll();
    Task<PhotoDtoOut> CreatePhoto(PhotoDtoIn photo, string jwt);
    Task DeletePhoto(string id, string jwt);
  }
}
