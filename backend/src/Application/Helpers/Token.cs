using Application.Dtos;
using Application.Helpers.Interfaces;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
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

    public async Task<UserRegisterDtoOut> Create(UserRegisterDtoOut user)
    {
      var tokenHandler = new JwtSecurityTokenHandler();
      var st = _configuration.GetSection("Security").Value;
      var key = Encoding.ASCII.GetBytes(st.ToString());

      var claims = new List<Claim>() {
        new Claim("id", user.Id),
        new Claim("name", user.Name),
        new Claim("email", user.Email),
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
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
      };
      var token = tokenHandler.CreateToken(tokenDescriptor);
      user.Jwt = tokenHandler.WriteToken(token);

      return user;
    }

    public JwtSecurityToken Read(string token)
    {
      Validate(token);
      return new JwtSecurityTokenHandler().ReadJwtToken(token);
    }

    public string GetClaim(string token, string claim)
    {
      try
      {
        var identity = Read(token);
        IEnumerable<Claim> claims = identity.Claims;
        var result = claims.Where(x => x.Type == claim).FirstOrDefault().Value;
        return result;
      }
      catch
      {
        return "";
      }
    }

    private void Validate(string token)
    {
      var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration.GetSection("Security").Value));

      var validationParameters = new TokenValidationParameters()
      {
        IssuerSigningKey = mySecurityKey,
        ValidateLifetime = true,
        ValidateAudience = false,
        ValidateIssuer = false,
        ValidateIssuerSigningKey = true
      };
      var tokenHandler = new JwtSecurityTokenHandler();
      SecurityToken validatedToken = null;
      try
      {
        tokenHandler.ValidateToken(token, validationParameters, out validatedToken);
      }
      catch (SecurityTokenException)
      {
        throw;
      }
      catch (Exception e)
      {
        throw;
      }
    }
  }
}
