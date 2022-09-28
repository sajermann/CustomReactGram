using Application.Dtos;

namespace Application.Interfaces
{
  public interface IPhotoService
  {
    Task<IList<PhotoDtoOut>> GetAll();
    Task<IList<PhotoDtoOut>> GetAllByUserId(string userId);
    Task<PhotoDtoOut> GetById(string id);
    Task<PhotoDtoOut> CreatePhoto(PhotoDtoIn photo, string jwt);
    Task DeletePhoto(string id, string jwt);
  }
}
