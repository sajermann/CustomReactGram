using Application.Dtos;

namespace Application.Interfaces
{
  public interface IUserService
  {
    void RegisterAndSignIn(UserRegisterDtoIn userRegisterAndSignIn);
    //Task<List<UserDtoOut>> GetAll();
    //Task<UserDtoOut> GetById(Guid id);
    //Task<UserDtoOut> Create(UserDtoIn model);
    //Task<UserDtoOut> Update(UserDtoIn model, Guid id);
    //Task Delete(Guid id);
    //Task<UserDtoOut> UploadAvatar(IFormFile avatar, string jwt);
  }
}
