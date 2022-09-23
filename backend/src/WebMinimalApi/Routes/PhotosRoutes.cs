using Api.Helpers.Interfaces;
using Application.Helpers;
using Application.Interfaces;
using WebMinimalApi.Helpers;

namespace Api.Routes
{
  public static class PhotosRoutes
  {
    public static void AddPhotosRoutes(this WebApplication app)
    {
      var BaseURL = "/api/photos";


      
      app.MapPost($"{BaseURL}/upload", async (ICustomException customException, IPhotoService photoService, HttpRequest request) =>
      {
        try
        {
          var photoDtoIn = await request.FormExtractor();
          var tokenJwt = request.JwtExtractor();
          var result = await photoService.CreatePhoto(photoDtoIn, tokenJwt);
          return Results.Ok(result);
        }
        catch (ModelValidationException ex)
        {
          return Results.ValidationProblem(ex.Errors, null, null, ex.StatusCode);
        }
      })
      .RequireAuthorization()
      .WithTags("Photos")
      .Produces(StatusCodes.Status401Unauthorized)
      .Produces(StatusCodes.Status400BadRequest);

    }

  }

}