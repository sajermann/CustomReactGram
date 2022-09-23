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
      photoNew.Image = photo.Image;
      photoNew.Title = photo.Title;
      photoNew.Likes = photo.Likes;
      photoNew.Comments = photo.Comments;
      photoNew.UserId = photo.UserId;
      photoNew.UserName = photo.UserName;
      return photoNew;
    }
  }
}
