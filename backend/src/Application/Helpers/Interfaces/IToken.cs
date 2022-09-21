using Application.Dtos;
using System.IdentityModel.Tokens.Jwt;

namespace Application.Helpers.Interfaces
{
  public interface IToken
  {
    Task<UserRegisterDtoOut> GenerateToken(UserRegisterDtoOut userAuth);

    JwtSecurityToken ReadToken(string token);

    //bool ValidateToken(Logged logged);

  }
}
