using Application.Dtos;
using Application.Helpers;
using Application.Helpers.Interfaces;
using Application.Interfaces;
using Data;
using Domain;
using System.Linq;
using System.Linq.Expressions;

namespace Application
{
  public class PhotoService : IPhotoService
  {
    private readonly IUserRepository _userRepository;
    private readonly IPhotoRepository _photoRepository;
    private readonly IToken _token;


    public PhotoService(IToken token, IUserRepository userRepository, IPhotoRepository photoRepository)
    {
      _token = token;
      _userRepository = userRepository;
      _photoRepository = photoRepository;
    }

    public async Task<IList<PhotoDtoOut>> GetAll()
    {
      var result = await _photoRepository.GetAll();
      return result.ToPhotosDtoOut();
    }

    public async Task<IList<PhotoDtoOut>> GetAllByUserId(string userId)
    {
      var result = await _photoRepository.GetAllByUserId(userId);
      return result.ToPhotosDtoOut();
    }

    public async Task<PhotoDtoOut> GetById(string id)
    {
      var result = await _photoRepository.GetById(id);
      return result.ToPhotoDtoOut();
    }

    public async Task<PhotoDtoOut> CreatePhoto(PhotoDtoIn photoDtoIn, string jwt)
    {
      var loggedUser = _token.GetClaim(jwt, "id");

      var user = await _userRepository.GetById(loggedUser);
      if (user == null) throw new Exception("User not found!");

      var completeNameFile = await photoDtoIn.Image.SuperCopyAsync("Photos");
      var photo = new Photo();
      photo.Image = completeNameFile;
      photo.Title = photoDtoIn.Title;
      photo.UserId = user.Id;
      photo.UserName = user.Name;
      var result = await _photoRepository.Create(photo);
      var photoDto = result.ToPhotoDtoOut();
      return photoDto;
    }

    public async Task DeletePhoto(string id, string jwt)
    {
      var loggedUser = _token.GetClaim(jwt, "id");

      var user = await _userRepository.GetById(loggedUser);
      if (user == null) throw new Exception("User not found!");

      var photo = await _photoRepository.GetById(id);
      if(photo == null || photo.UserId != loggedUser) throw new Exception("Error Strange");

      var result = await _photoRepository.Delete(id);

      if(!result) throw new Exception("A Photo não pode ser excluída");
    }

    public async Task<PhotoDtoOut> Update(PhotoUpdateDtoIn photoUpdateDtoIn, string id, string jwt)
    {
      var loggedUser = _token.GetClaim(jwt, "id");

      var user = await _userRepository.GetById(loggedUser);
      if (user == null) throw new Exception("User not found!");

      var photo = await _photoRepository.GetById(id);
      if (photo == null || photo.UserId != loggedUser) throw new Exception("Photo não encontrada");

      photo.Title = photoUpdateDtoIn.Title;
      photo.UpdatedAt = DateTime.Now;

      var result = await _photoRepository.Update(photo);
      return result.ToPhotoDtoOut();

    }

    public async Task<PhotoDtoOut> Like(string id, string jwt)
    {
      var loggedUser = _token.GetClaim(jwt, "id");

      var user = await _userRepository.GetById(loggedUser);
      if (user == null) throw new Exception("User not found!");

      var photo = await _photoRepository.GetById(id);
      if(photo.Likes == null)
      {
        var likes = new List<string>();
        likes.Add(user.Id);
        photo.Likes = likes;
      }
      else
      {
        var likeExist = photo.Likes.Where(b => b == user.Id).FirstOrDefault();

        if (likeExist == null)
        {
          photo.Likes.Add(user.Id);
        }
        else
        {
          photo.Likes.Remove(user.Id);
        }
      }
      photo.UpdatedAt = DateTime.Now;
      var photoSaved = await _photoRepository.Update(photo);

      return photoSaved.ToPhotoDtoOut();
    }

    public async Task<PhotoDtoOut> Comment(PhotoCommentDtoIn photoCommentDtoIn, string id, string jwt)
    {
      var loggedUser = _token.GetClaim(jwt, "id");

      var user = await _userRepository.GetById(loggedUser);
      if (user == null) throw new Exception("User not found!");

      var photo = await _photoRepository.GetById(id);
      if (photo == null) throw new Exception("Photo não encontrada");

      var comment = new
      {
        comment = photoCommentDtoIn.Comment,
        userName = user.Name,
        userImage = user.ProfileImage,
        userId = user.Id,
      };

      if (photo.Comments == null)
      {
        var comments = new List<dynamic>();
        comments.Add(comment);
        photo.Comments = comments;
      }
      else
      {
        photo.Comments.Add(comment);
      }

      var result = await _photoRepository.Update(photo);
      return result.ToPhotoDtoOut();

    }

    public async Task<IList<PhotoDtoOut>> GetByTitle(string title)
    {
      Expression<Func<Photo, bool>> filter = x => x.Title.ToLower().Contains(title.ToLower());
      var result = await _photoRepository.GetByFilter(filter);
      return result.ToPhotosDtoOut();
    }
  }
}
