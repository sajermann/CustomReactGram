using Application.Dtos;
using Domain;

namespace Application.Helpers
{
  public static class UserExtension
  {
    public static UserDtoOut ToUserDtoOut(this User user)
    {
      var userNew = new UserDtoOut();
      userNew.Id = user.Id;
      userNew.CreatedAt = user.CreatedAt;
      userNew.UpdatedAt = user.UpdatedAt;
      userNew.Name = user.Name;
      userNew.Bio = user.Bio;
      userNew.Email = user.Email;

      return userNew;
    }
  }
}
