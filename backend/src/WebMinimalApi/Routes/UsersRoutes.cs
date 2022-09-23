using Api.Helpers.Interfaces;
using Application.Dtos;
using Application.Helpers;
using Application.Interfaces;
using WebMinimalApi.Helpers;

namespace Api.Routes
{

  public static class UsersRoutes
  {
    public static void AddUsersRoutes(this WebApplication app)
    {
      var BaseURL = "/api/users";

      app.MapPost($"{BaseURL}/register", async (IUserService userService, UserRegisterDtoIn userRegister) =>
      {
        try
        {
          userRegister.IsValid();
          var result = await userService.RegisterAndSignIn(userRegister);
          return Results.Ok(result);
        }
        catch (ModelValidationException ex)
        {
          return Results.ValidationProblem(ex.Errors, null, null, ex.StatusCode);
        }
      }).WithTags("Users");

      app.MapPost($"{BaseURL}/login", async (IUserService userService, UserLogin userLogin) =>
      {
        try
        {
          userLogin.IsValid();
          var result = await userService.Login(userLogin);
          return Results.Ok(result);
        }
        catch (ModelValidationException ex)
        {
          return Results.ValidationProblem(ex.Errors, null, null, ex.StatusCode);
        }
      }).WithTags("Users");

      app.MapGet($"{BaseURL}/profile", async (IUserService userService, HttpRequest request) =>
      {
        var tokenJwt = request.JwtExtractor();
        var result = await userService.GetProfile(tokenJwt);
        return Results.Ok(result);
      }).RequireAuthorization().WithTags("Users").Produces(StatusCodes.Status401Unauthorized);

      app.MapGet($"{BaseURL}/{{id}}", async (IUserService userService, string id) =>
      {
        try
        {
          var result = await userService.GetById(id);
          return Results.Ok(result);
        }
        catch (ModelValidationException ex)
        {
          return Results.ValidationProblem(ex.Errors, null, null, ex.StatusCode);
        }
      }).RequireAuthorization().WithTags("Users").Produces(StatusCodes.Status401Unauthorized);

      app.MapPut($"{BaseURL}", async (IUserService userService, HttpRequest request, UserDtoIn userUpdate) =>
      {
        var tokenJwt = request.JwtExtractor();
        var result = await userService.UpdateProfile(userUpdate, tokenJwt);
        return Results.Ok(result);
      }).RequireAuthorization().WithTags("Users").Produces(StatusCodes.Status401Unauthorized);

      app.MapPost($"{BaseURL}/upload", async (ICustomException customException, IUserService userService, HttpRequest request) =>
      {
        try
        {
          if (!request.HasFormContentType)
          {
            return Results.BadRequest();
          }
          var form = await request.ReadFormAsync();
          var formFile = form.Files["file"];
          if (formFile is null || formFile.Length == 0)
            return Results.BadRequest();
          var tokenJwt = request.JwtExtractor();
          var result = await userService.UploadAvatar(formFile, tokenJwt);
          return Results.Ok(result);

        }
        catch (Exception ex)
        {
          return customException.Error(ex.Message);
        }
      })
      .RequireAuthorization()
      .WithTags("Users")
      .Produces(StatusCodes.Status401Unauthorized)
      .Produces(StatusCodes.Status400BadRequest);

    }

  }

}