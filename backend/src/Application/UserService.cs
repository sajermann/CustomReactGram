using Application.Dtos;
using Application.Helpers.Interfaces;
using Application.Interfaces;

namespace Application
{
  public class UserService : IUserService
  {
    //private readonly IGenericRepository<User> _genericRepository;
    //private readonly IMapper _mapper;
    private readonly IToken _token;


    public UserService(IToken token)
    {
      _token = token;
    }

    public void RegisterAndSignIn(UserRegisterDtoIn userRegisterAndSignIn)
    {
      var t = userRegisterAndSignIn;
    }

    //public async Task<List<UserDtoOut>> GetAll()
    //{
    //  var results = _mapper.Map<List<UserDtoOut>>(await _genericRepository.Find());
    //  return results;
    //}

    //public async Task<UserDtoOut> GetById(Guid id)
    //{
    //  var result = _mapper.Map<UserDtoOut>((await _genericRepository.Find(b => b.Id == id)).FirstOrDefault());
    //  return result;
    //}

    //public async Task<UserDtoOut> Create(UserDtoIn model)
    //{
    //  var checkUserExists = (await _genericRepository.Find(b => b.Email == model.Email)).FirstOrDefault();
    //  if (checkUserExists != null) throw new Exception("Email address already used!");
    //  var userForInsert = _mapper.Map<User>(model);
    //  userForInsert.Password = Password.Encripty(model.Password);
    //  var userInserted = await _genericRepository.AddOrUpdate(userForInsert);
    //  return _mapper.Map<UserDtoOut>(userInserted);
    //}


    //public async Task<UserDtoOut> Update(UserDtoIn model, Guid id)
    //{
    //  Expression<Func<User, User>> select = b => new User { Id = b.Id };
    //  Expression<Func<User, bool>> where = b => b.Username == model.Username && b.Id != model.Id;
    //  var usernameExist = await _genericRepositoryNew.Find(where: where, selects: select);
    //  if (usernameExist.Count > 0) throw new Exception("Username informado já encontra-se em uso!");

    //  var resultOld = await _userRepository.GetById(model.Id);
    //  if (resultOld == null) throw new Exception("ID não localizado!");
    //  var userForUpdate = _mapper.Map<User>(model);
    //  userForUpdate.Password = resultOld.Password;
    //  userForUpdate.CreatedAt = resultOld.CreatedAt;
    //  userForUpdate.UpdatedAt = DateTime.UtcNow;

    //  await _emailService.DeleteAllByUserId(userForUpdate.Id);
    //  await _phoneService.DeleteAllByUserId(userForUpdate.Id);
    //  await _permissionUserService.DeleteAllByUserId(userForUpdate.Id);

    //  var permissionsComplete = new List<Permission>();

    //  foreach (var item in userForUpdate.Permissions)
    //  {
    //    Expression<Func<Permission, bool>> whereThis = b => b.Id == item.Id && b.IsActive;
    //    var permiss = await _genericRepositoryNewPermission.Find(where: whereThis);
    //    if (permiss.Count > 0) permissionsComplete.Add(permiss.FirstOrDefault());
    //  }
    //  userForUpdate.Permissions = permissionsComplete;
    //  var userUpdated = await _genericRepositoryNew.AddOrUpdate(userForUpdate);
    //  ClearCache(userUpdated.Id.ToString(), userUpdated.Username);
    //  return _mapper.Map<UserDtoOut>((await _genericRepository.Find(b => b.Id == id)).FirstOrDefault());
    //}

    //public async Task Delete(Guid id)
    //{
    //  Expression<Func<User, bool>> where = b => b.Id == id && b.IsActive;
    //  var results = await _genericRepositoryNew.Find(where: where);
    //  if (results.Count == 0) throw new Exception("Id não localizado!");
    //  var modelForDelete = results.FirstOrDefault();
    //  modelForDelete.UpdatedAt = DateTime.UtcNow;
    //  modelForDelete.IsActive = false;
    //  var userDeleted = await _genericRepositoryNew.AddOrUpdate(modelForDelete);
    //  ClearCache(userDeleted.Id.ToString(), userDeleted.Username);
    //}

    //public async Task<UserDtoOut> UploadAvatar(IFormFile avatar, string jwt)
    //{
    //  var loggedUser = _token.ReadToken(jwt);
    //  var loggedUserId = Convert.ToString(loggedUser.Claims.Where(b => b.Type == "id").FirstOrDefault().Value);

    //  var user = (await _genericRepository.Find(b => b.Id == Guid.Parse(loggedUserId))).FirstOrDefault();
    //  if (user == null) throw new Exception("User not found!");

    //  var completeNameFile = loggedUserId + Path.GetExtension(avatar.FileName);
    //  var currentDirectory = Environment.CurrentDirectory;
    //  var fullPath = Path.Combine(currentDirectory, "..", "GoBarber.Data", "Database", "ImgAvatars", completeNameFile);

    //  using (var fileStream = new FileStream(fullPath, FileMode.Create))
    //  {
    //    await avatar.CopyToAsync(fileStream);
    //  }
    //  user.Avatar = completeNameFile;
    //  var result = await _genericRepository.AddOrUpdate(user);
    //  return _mapper.Map<UserDtoOut>(result);
    //}
  }
}
