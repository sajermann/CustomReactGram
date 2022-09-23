using Application.Dtos;
using Application.Helpers;
using Application.Helpers.Interfaces;
using Application.Interfaces;
using Data;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application
{
  public class UserService : IUserService
  {
    private readonly IUserRepository _userRepository;
    private readonly IToken _token;


    public UserService(IToken token, IUserRepository userRepository)
    {
      _token = token;
      _userRepository = userRepository;
    }

    public async Task<UserRegisterDtoOut> RegisterAndSignIn(UserRegisterDtoIn userRegisterAndSignIn)
    {
      var userExists = await _userRepository.GetByEmail(userRegisterAndSignIn.Email);
      if (userExists != null)
      {
        var errorsNew = new Dictionary<string, string[]>();
        List<string> errorsEmails = new List<string>();
        errorsEmails.Add("Address already used!");
        errorsNew.Add("Email", errorsEmails.ToArray());
        throw new ModelValidationException(400, errorsNew);
      }
      var user = new User();
      user.Name = userRegisterAndSignIn.Name;
      user.Email = userRegisterAndSignIn.Email;
      user.Password = Password.Encripty(userRegisterAndSignIn.Password);
      await _userRepository.Create(user);

      var userInserted = await _userRepository.GetByEmail(userRegisterAndSignIn.Email);
      var userTemp = new UserRegisterDtoOut();
      userTemp.Name = userInserted.Name;
      userTemp.Email = userInserted.Email;
      userTemp.Id = userInserted.Id;
      var result = await _token.Create(userTemp);
      return result;
    }

    public async Task<UserRegisterDtoOut> Login(UserLogin userLogin)
    {
      var userExists = await _userRepository.GetByEmail(userLogin.Email);
      if (userExists == null)
      {
        var errorsNew = new Dictionary<string, string[]>();
        List<string> errorsEmails = new List<string>();
        errorsEmails.Add("Email or password invalid!");
        errorsNew.Add("Email/Password", errorsEmails.ToArray());
        throw new ModelValidationException(400, errorsNew);
      }

      var isValidPass = Password.VerifyPass(userLogin.Password, userExists.Password);
      if (!isValidPass)
      {
        var errorsNew = new Dictionary<string, string[]>();
        List<string> errorsEmails = new List<string>();
        errorsEmails.Add("Email or password invalid!");
        errorsNew.Add("Email/Password", errorsEmails.ToArray());
        throw new ModelValidationException(400, errorsNew);
      }

      var userTemp = new UserRegisterDtoOut();
      userTemp.Name = userExists.Name;
      userTemp.Email = userExists.Email;
      userTemp.Id = userExists.Id;
      userTemp.ProfileImage = userExists.ProfileImage;
      userTemp.Bio = userExists.Bio;
      var result = await _token.Create(userTemp);
      return result;
    }

    public async Task<UserDtoOut> GetById(string id)
    {
      var user = await _userRepository.GetById(id);
      if (user == null)
      {
        var errorsNew = new Dictionary<string, string[]>();
        List<string> errors = new List<string>();
        errors.Add("Usuário não localizado!");
        errorsNew.Add("User", errors.ToArray());
        throw new ModelValidationException(404, errorsNew);
      }
      return user.ToUserDtoOut();
    }

    public async Task<UserDtoOut> GetProfile(string jwt)
    {
      var userId = _token.GetClaim(jwt, "id");
      var user = await _userRepository.GetById(userId);
      return user.ToUserDtoOut();
    }

    public async Task<UserDtoOut> GetByEmail(string email)
    {
      var user = await _userRepository.GetByEmail(email);
      return user.ToUserDtoOut();
    }

    public async Task<UserDtoOut> UpdateProfile(UserDtoIn userForUpdate, string jwt)
    {
      var userId = _token.GetClaim(jwt, "id");
      var user = await _userRepository.GetById(userId);

      user.ProfileImage = userForUpdate.ProfileImage;
      user.Bio = userForUpdate.Bio;
      user.Name = userForUpdate.Name;
      user.UpdatedAt = DateTime.Now;

      var result = await _userRepository.Update(user);
      if (!result)
      {
        var errorsNew = new Dictionary<string, string[]>();
        List<string> errors = new List<string>();
        errors.Add("Ocorreu um erro ao salvar o usuário");
        errorsNew.Add("UpdateUser", errors.ToArray());
        throw new ModelValidationException(500, errorsNew);
      }
      return user.ToUserDtoOut();

    }

    public async Task<string> UploadAvatar(IFormFile avatar, string jwt)
    {
      var loggedUser = _token.GetClaim(jwt, "id");

      var user = await _userRepository.GetById(loggedUser);
      if (user == null) throw new Exception("User not found!");

      var completeNameFile = Guid.NewGuid() + Path.GetExtension(avatar.FileName);
      var currentDirectory = Environment.CurrentDirectory;
      var fullPath = Path.Combine(currentDirectory, "..", "Data", "Uploads", "Users", completeNameFile);

      using (var fileStream = new FileStream(fullPath, FileMode.Create))
      {
        await avatar.CopyToAsync(fileStream);
      }
      user.ProfileImage = completeNameFile;
      user.UpdatedAt = DateTime.Now;
      var result = await _userRepository.Update(user);
      return completeNameFile;
    }
  }
}
