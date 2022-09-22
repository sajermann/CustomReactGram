using Application.Dtos;
using System.IdentityModel.Tokens.Jwt;

namespace Application.Helpers.Interfaces
{
  public interface IToken
  {
    Task<UserRegisterDtoOut> Create(UserRegisterDtoOut userAuth);

    JwtSecurityToken Read(string token);

    string GetClaim(string token, string claim);

  }
}
