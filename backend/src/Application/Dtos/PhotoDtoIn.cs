using Application.Helpers;
using Microsoft.AspNetCore.Http;

namespace Application.Dtos
{
  public class PhotoDtoIn
  {
    public string Title { get; set; }
    public IFormFile Image { get; set; }

    public void IsValid()
    {
      var errorsNew = new Dictionary<string, string[]>();

      List<string> errorsTitle = new List<string>();
      if (Title != null && Title.Length < 3) errorsTitle.Add("Tamanho minímo de 3 caracteres.");
      if (Title != null && Title.Length > 20) errorsTitle.Add("Tamanho máximo de 20 caracteres.");
      if (Title == null) errorsTitle.Add("É obrigatório.");
      if (errorsTitle.Count > 0) errorsNew.Add("Title", errorsTitle.ToArray());

      List<string> errorsImage = new List<string>();
      if (Image == null) errorsImage.Add("É obrigatório.");
      if (errorsImage.Count > 0) errorsNew.Add("Image", errorsImage.ToArray());

      if (errorsNew.Count > 0) throw new ModelValidationException(400, errorsNew);
    }   
  }
}
