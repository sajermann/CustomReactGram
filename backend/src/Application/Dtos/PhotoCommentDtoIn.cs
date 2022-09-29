using Application.Helpers;


namespace Application.Dtos
{
  public class PhotoCommentDtoIn
  {
    public string Comment { get; set; }

    public void IsValid()
    {
      var errorsNew = new Dictionary<string, string[]>();

      List<string> errorsTitle = new List<string>();
      if (Comment != null && Comment.Length < 3) errorsTitle.Add("Tamanho minímo de 3 caracteres.");
      if (Comment != null && Comment.Length > 20) errorsTitle.Add("Tamanho máximo de 100 caracteres.");
      if (Comment == null) errorsTitle.Add("É obrigatório.");
      if (errorsTitle.Count > 0) errorsNew.Add("Comment", errorsTitle.ToArray());

      if (errorsNew.Count > 0) throw new ModelValidationException(400, errorsNew);
    }   
  }
}
