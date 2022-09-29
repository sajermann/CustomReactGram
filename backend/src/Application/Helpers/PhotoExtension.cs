using Application.Dtos;
using Domain;
using Microsoft.AspNetCore.Http;

namespace Application.Helpers
{
  public static class PhotoExtension
  {
    public async static Task<string> SuperCopyAsync(this IFormFile photo, string location)
    {
      var completeNameFile = Guid.NewGuid() + Path.GetExtension(photo.FileName);
      var currentDirectory = Environment.CurrentDirectory;
      var fullPath = Path.Combine(currentDirectory, "..", "Data", "Uploads", location, completeNameFile);

      using (var fileStream = new FileStream(fullPath, FileMode.Create))
      {
        await photo.CopyToAsync(fileStream);
      }
      return completeNameFile;
    }
    public static PhotoDtoOut ToPhotoDtoOut(this Photo photo)
    {
      var photoNew = new PhotoDtoOut();
      if (photo == null) return null;

      photoNew.Id = photo.Id;
      photoNew.Image = photo.Image;
      photoNew.Title = photo.Title;
      photoNew.Likes = photo.Likes;
      photoNew.Comments = photo.Comments;
      photoNew.UserId = photo.UserId;
      photoNew.UserName = photo.UserName;
      return photoNew;
    }

    public static IList<PhotoDtoOut> ToPhotosDtoOut(this IList<Photo> photos)
    {
      var photosNew = new List<PhotoDtoOut>();
      
      for(int i = 0; i < photos.Count; i++)
      {
        var photoNew = new PhotoDtoOut();
        photoNew.Image = photos[i].Image;
        photoNew.Title = photos[i].Title;
        photoNew.Likes = photos[i].Likes;
        photoNew.Comments = photos[i].Comments;
        photoNew.UserId = photos[i].UserId;
        photoNew.UserName = photos[i].UserName;
        photosNew.Add(photoNew);
      }

      return photosNew;
    }
  }
}
