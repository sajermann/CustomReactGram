using Api.Helpers.Interfaces;
using Application.Dtos;
using Application.Helpers;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebMinimalApi.Helpers;

namespace Api.Routes
{
  public static class PhotosRoutes
  {
    public static void AddPhotosRoutes(this WebApplication app)
    {
      var BaseURL = "/api/photos";

      app.MapGet($"{BaseURL}/", async (ICustomException customException, IPhotoService photoService) =>
      {
        try
        {
          var result = await photoService.GetAll();
          return Results.Ok(result);
        }
        catch (Exception ex)
        {
          return Results.BadRequest(ex.Message);
        }
      })
      .RequireAuthorization()
      .WithTags("Photos")
      .Produces(StatusCodes.Status401Unauthorized)
      .Produces(StatusCodes.Status400BadRequest);

      app.MapGet($"{BaseURL}/user/{{userId}}", async (ICustomException customException, [FromRoute] string userId, IPhotoService photoService) =>
      {
        try
        {
          var result = await photoService.GetAllByUserId(userId);
          return Results.Ok(result);
        }
        catch (Exception ex)
        {
          return Results.BadRequest(ex.Message);
        }
      })
      .RequireAuthorization()
      .WithTags("Photos")
      .Produces(StatusCodes.Status401Unauthorized)
      .Produces(StatusCodes.Status400BadRequest);

      app.MapGet($"{BaseURL}/{{id}}", async (ICustomException customException, [FromRoute] string id, IPhotoService photoService) =>
      {
        try
        {
          var result = await photoService.GetById(id);
          if (result == null) return Results.NotFound();
          return Results.Ok(result);
        }
        catch (Exception ex)
        {
          return Results.BadRequest(ex.Message);
        }
      })
      .RequireAuthorization()
      .WithTags("Photos")
      .Produces(StatusCodes.Status401Unauthorized)
      .Produces(StatusCodes.Status400BadRequest);

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
        catch (Exception ex)
        {
          return Results.BadRequest(ex.Message);
        }
      })
      .RequireAuthorization()
      .WithTags("Photos")
      .Produces(StatusCodes.Status401Unauthorized)
      .Produces(StatusCodes.Status400BadRequest);

      app.MapPut($"{BaseURL}/{{id}}", async (ICustomException customException, IPhotoService photoService, [FromRoute] string id, [FromBody] PhotoUpdateDtoIn photoUpdateDtoIn, HttpRequest request) =>
      {
        try
        {
          var tokenJwt = request.JwtExtractor();
          photoUpdateDtoIn.IsValid();
          var result = await photoService.Update(photoUpdateDtoIn, id, tokenJwt);
          return Results.Ok(result);
        }
        catch (ModelValidationException ex)
        {
          return Results.ValidationProblem(ex.Errors, null, null, ex.StatusCode);
        }
        catch (Exception ex)
        {
          return Results.BadRequest(ex.Message);
        }
      })
     .RequireAuthorization()
     .WithTags("Photos")
     .Produces(StatusCodes.Status401Unauthorized)
     .Produces(StatusCodes.Status400BadRequest);

      app.MapPut($"{BaseURL}/like/{{id}}", async (ICustomException customException, IPhotoService photoService, [FromRoute] string id, HttpRequest request) =>
      {
        try
        {
          var tokenJwt = request.JwtExtractor();
          var result = await photoService.Like(id, tokenJwt);
          return Results.Ok(result);
        }
        catch (ModelValidationException ex)
        {
          return Results.ValidationProblem(ex.Errors, null, null, ex.StatusCode);
        }
        catch (Exception ex)
        {
          return Results.BadRequest(ex.Message);
        }
      })
      .RequireAuthorization()
      .WithTags("Photos")
      .Produces(StatusCodes.Status401Unauthorized)
      .Produces(StatusCodes.Status400BadRequest);

      app.MapPut($"{BaseURL}/comment/{{id}}", async (ICustomException customException, IPhotoService photoService, [FromRoute] string id, [FromBody] PhotoCommentDtoIn photoCommentDtoIn, HttpRequest request) =>
      {
        try
        {
          var tokenJwt = request.JwtExtractor();
          photoCommentDtoIn.IsValid();
          var result = await photoService.Comment(photoCommentDtoIn, id, tokenJwt);
          return Results.Ok(result);
        }
        catch (ModelValidationException ex)
        {
          return Results.ValidationProblem(ex.Errors, null, null, ex.StatusCode);
        }
        catch (Exception ex)
        {
          return Results.BadRequest(ex.Message);
        }
      })
     .RequireAuthorization()
     .WithTags("Photos")
     .Produces(StatusCodes.Status401Unauthorized)
     .Produces(StatusCodes.Status400BadRequest);

      app.MapDelete($"{BaseURL}/{{id}}", async (ICustomException customException, [FromRoute] string id, IPhotoService photoService, HttpRequest request) =>
      {
        try
        {
          var tokenJwt = request.JwtExtractor();
          await photoService.DeletePhoto(id, tokenJwt);
          return Results.NoContent();
        }
        catch (Exception ex)
        {
          return Results.BadRequest(ex.Message);
        }
      })
      .RequireAuthorization()
      .WithTags("Photos")
      .Produces(StatusCodes.Status401Unauthorized)
      .Produces(StatusCodes.Status400BadRequest);

      app.MapGet($"{BaseURL}/search", async (ICustomException customException, [FromQuery] string title, IPhotoService photoService) =>
      {
        try
        {
          var result = await photoService.GetByTitle(title);
          if (result == null) return Results.NotFound();
          return Results.Ok(result);
        }
        catch (Exception ex)
        {
          return Results.BadRequest(ex.Message);
        }
      })
      .RequireAuthorization()
      .WithTags("Photos")
      .Produces(StatusCodes.Status401Unauthorized)
      .Produces(StatusCodes.Status400BadRequest);

    }

  }

}