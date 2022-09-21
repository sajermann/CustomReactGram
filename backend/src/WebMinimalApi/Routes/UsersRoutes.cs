using Api.Helpers.Interfaces;
using Application.Dtos;
using Application.Helpers;
using Application.Interfaces;

namespace Api.Routes
{

  public static class UsersRoutes
  {
    public static void AddUsersRoutes(this WebApplication app)
    {
      var BaseURL = "/api/users";
      //app.MapGet($"{BaseURL}", (
      //  // ICustomException customException, IUserService userService
      //  ) =>
      //{
      //  try
      //  {
      //    //var users = userService.GetAll();
      //    return Results.Ok("Bala");
      //  }
      //  catch (Exception ex)
      //  {
      //    var me = ex.Message;
      //    //return customException.Error(ex.Message);
      //    return Results.Ok("Erro");
      //  }
      //}).WithDisplayName("Usuários").WithTags("Users");

      app.MapPost($"{BaseURL}/register", async (IUserService userService, UserRegisterDtoIn userRegister) =>
      {
        try
        {
          userRegister.IsValid();
          await userService.RegisterAndSignIn(userRegister);
          return Results.Ok("Bala");
        }
        catch (ModelValidationException ex)
        {
          return Results.ValidationProblem(ex.Errors, null, null, ex.StatusCode);
        }
      }).WithDisplayName("Usuários").WithTags("Users");

      //app.MapPost($"{BaseURL}", (IUserService userService, UserDtoIn user) =>
      //{
      //  try
      //  {

      //    user.IsValid();
      //    var result = userService.Create(user);
      //    return Results.Ok(result);

      //  }
      //  catch (ModelValidationException ex)
      //  {
      //    return Results.Problem(null, null, ex.StatusCode, null, null, ex.Errors);
      //  }

      //})
      //.WithDisplayName("Users")
      //.WithTags("Users")
      //.Produces<IDictionary<string, object>>(StatusCodes.Status400BadRequest)
      //.Produces<UserDtoOut>(StatusCodes.Status201Created);

      //app.MapPost($"{BaseURL}/avatar", async (ICustomException customException, IUserService userService, HttpRequest request) =>
      //{
      //  try
      //  {
      //  if (!request.HasFormContentType)
      //  {
      //    return Results.BadRequest();
      //  }
      //  var form = await request.ReadFormAsync();
      //  var formFile = form.Files["file"];
      //  if (formFile is null || formFile.Length == 0)
      //    return Results.BadRequest();
      //  var tokenJwt = request.Headers["Authorization"].ToString().Replace("Bearer ", "");
      //  var result = await userService.UploadAvatar(formFile, tokenJwt);

      //  return Results.Ok(result);
      //  }
      //  catch(Exception ex)
      //  {
      //    return customException.Error(ex.Message);
      //  }
      //})
      //.RequireAuthorization()
      //.WithDisplayName("Users")
      //.WithTags("Users")
      //.Produces(StatusCodes.Status401Unauthorized)
      //.Produces(StatusCodes.Status400BadRequest)
      //.Produces<UserDtoOut>(StatusCodes.Status201Created);

    }

  }

}