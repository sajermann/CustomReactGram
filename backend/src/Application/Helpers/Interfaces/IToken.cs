using Application.Dtos;
using System.IdentityModel.Tokens.Jwt;

namespace Application.Helpers.Interfaces
{
  public interface IToken
  {
    Task<UserDtoOut> GenerateToken(UserDtoOut userAuth);

    JwtSecurityToken ReadToken(string token);

    //bool ValidateToken(Logged logged);

  }
}
