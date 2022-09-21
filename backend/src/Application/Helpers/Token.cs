using Application.Dtos;
using Application.Helpers.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Helpers
{
  public class Token : IToken
  {
    private readonly IConfiguration _configuration;
    public Token(IConfiguration configuration)
    {
      _configuration = configuration;
    }

    public async Task<UserDtoOut> GenerateToken(UserDtoOut user)
    {
      var tokenHandler = new JwtSecurityTokenHandler();
      var st = _configuration.GetSection("Security").Value;
      var key = Encoding.ASCII.GetBytes(st.ToString());

      var claims = new List<Claim>() {
        new Claim("id", user.Id.ToString()),
        new Claim("name", user.Name),
      };

      //#region Arrays of Claims
      //for (var i = 0; i < user.Roles.Count(); i++)
      //{
      //  claims.Add(new Claim(ClaimTypes.Role, user.Roles[i].Name));
      //}
      //#endregion

      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(claims),
        Expires = DateTime.UtcNow.AddHours(2),
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
      };
      var token = tokenHandler.CreateToken(tokenDescriptor);
      user.Jwt = tokenHandler.WriteToken(token);

      return user;
    }

    public JwtSecurityToken ReadToken(string token)
    {
      return new JwtSecurityTokenHandler().ReadJwtToken(token);
    }

  }
}
