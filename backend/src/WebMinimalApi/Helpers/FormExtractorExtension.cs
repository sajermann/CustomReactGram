using Application.Dtos;

namespace WebMinimalApi.Helpers
{
  public static class FormExtractorExtension
  {

    public async static Task<PhotoDtoIn> FormExtractor(this HttpRequest request)
    {
      var photoDtoIn = new PhotoDtoIn();
      if (!request.HasFormContentType)
      {
        // return Results.BadRequest();
      }

      var form = await request.ReadFormAsync();
      var formFile = form.Files["file"];
      var keys = form["title"].FirstOrDefault();
      if (formFile is null || formFile.Length == 0)
      {
        // return Results.BadRequest();
      }

      photoDtoIn.Title = keys;
      photoDtoIn.Image = formFile;
      photoDtoIn.IsValid();
      return photoDtoIn;
    }
  }
}
