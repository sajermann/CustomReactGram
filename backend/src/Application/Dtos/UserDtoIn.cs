using Application.Helpers;

namespace Application.Dtos
{
  public class UserDtoIn
  {
    public string Name { get; set; }
    public string ProfileImage { get; set; }
    public string Bio { get; set; }

    public void IsValid()
    {
      var errorsNew = new Dictionary<string, string[]>();

      List<string> errorsName = new List<string>();
      if (Name != null && Name.Length < 3) errorsName.Add("Tamanho minímo de 3 caracteres.");
      if (Name != null && Name.Length > 20) errorsName.Add("Tamanho máximo de 20 caracteres.");
      if (Name == null) errorsName.Add("É obrigatório.");
      if (errorsName.Count > 0) errorsNew.Add("Name", errorsName.ToArray());

      List<string> errorsProfileImage = new List<string>();
      if (ProfileImage == null) errorsProfileImage.Add("É obrigatório.");
      if (errorsProfileImage.Count > 0) errorsNew.Add("Email", errorsProfileImage.ToArray());

      //List<string> errorsPassword = new List<string>();
      //if (Password != null && Password.Length > 50) errorsPassword.Add("Tamanho máximo de 50 caracteres.");
      //if (Password == null) errorsPassword.Add("É obrigatório.");
      //if (errorsPassword.Count > 0) errorsNew.Add("Password", errorsPassword.ToArray());


      if (errorsNew.Count > 0) throw new ModelValidationException(400, errorsNew);
    }   
  }
}
