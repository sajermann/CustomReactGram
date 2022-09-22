using Application.Dtos;

namespace Application.Interfaces
{
  public interface IUserService
  {
    Task<UserRegisterDtoOut> RegisterAndSignIn(UserRegisterDtoIn userRegisterAndSignIn);
    Task<UserRegisterDtoOut> Login(UserLogin userLogin);
    Task<UserDtoOut> GetById(string id);
    Task<UserDtoOut> GetByEmail(string email);
    Task<UserDtoOut> GetProfile(string jwt);
    Task<UserDtoOut> UpdateProfile(UserDtoIn user, string jwt);
    //Task<List<UserDtoOut>> GetAll();
    //Task<UserDtoOut> Create(UserDtoIn model);
    //Task<UserDtoOut> Update(UserDtoIn model, Guid id);
    //Task Delete(Guid id);
    //Task<UserDtoOut> UploadAvatar(IFormFile avatar, string jwt);
  }
}
