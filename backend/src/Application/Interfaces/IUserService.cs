using Application.Dtos;

namespace Application.Interfaces
{
  public interface IUserService
  {
    Task RegisterAndSignIn(UserRegisterDtoIn userRegisterAndSignIn);
    Task<UserDtoOut> GetById(string id);
    Task<UserDtoOut> GetByEmail(string email);
    //Task<List<UserDtoOut>> GetAll();
    //Task<UserDtoOut> Create(UserDtoIn model);
    //Task<UserDtoOut> Update(UserDtoIn model, Guid id);
    //Task Delete(Guid id);
    //Task<UserDtoOut> UploadAvatar(IFormFile avatar, string jwt);
  }
}
