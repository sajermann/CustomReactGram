using Application.Dtos;

namespace Application.Interfaces
{
  public interface IPhotoService
  {
    Task<IList<PhotoDtoOut>> GetAll();
    Task<IList<PhotoDtoOut>> GetAllByUserId(string userId);
    Task<PhotoDtoOut> GetById(string id);
    Task<PhotoDtoOut> CreatePhoto(PhotoDtoIn photo, string jwt);
    Task<PhotoDtoOut> Update(PhotoUpdateDtoIn photoUpdateDtoIn, string id, string jwt);
    Task<PhotoDtoOut> Like(string id, string jwt);
    Task<PhotoDtoOut> Comment(PhotoCommentDtoIn photoCommentDtoIn, string id, string jwt);
    Task DeletePhoto(string id, string jwt);
    Task<IList<PhotoDtoOut>> GetByTitle(string title);
  }
}
