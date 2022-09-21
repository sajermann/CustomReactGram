using Application.Helpers;

namespace Application.Dtos
{
  public class UserRegisterDtoIn
  {
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }

    public void IsValid()
    {
      var errorsNew = new Dictionary<string, string[]>();

      List<string> errorsName = new List<string>();
      if (Name != null && Name.Length < 3) errorsName.Add("Tamanho minímo de 3 caracteres.");
      if (Name != null && Name.Length > 20) errorsName.Add("Tamanho máximo de 20 caracteres.");
      if (Name == null) errorsName.Add("É obrigatório.");
      if(errorsName.Count > 0) errorsNew.Add("Name", errorsName.ToArray());

      List<string> errorsEmails = new List<string>();
      if (Email != null && Email.Length > 100) errorsEmails.Add("Tamanho máximo de 100 caracteres.");
      if (!IsValidEmail(Email)) errorsEmails.Add("Inválido");
      if (Email == null) errorsEmails.Add("É obrigatório.");
      if (errorsEmails.Count > 0) errorsNew.Add("Email", errorsEmails.ToArray());

      List<string> errorsPassword = new List<string>();
      if (Password != null && Password.Length > 50) errorsPassword.Add("Tamanho máximo de 50 caracteres.");
      if (Password == null) errorsPassword.Add("É obrigatório.");
      if (errorsPassword.Count > 0) errorsNew.Add("Password", errorsPassword.ToArray());

      List<string> errorsConfirmPassword = new List<string>();
      if (ConfirmPassword != null && ConfirmPassword != Password) errorsConfirmPassword.Add("Deve ser igual a senha.");
      if (ConfirmPassword == null) errorsConfirmPassword.Add("É obrigatório.");
      if (errorsConfirmPassword.Count > 0) errorsNew.Add("ConfirmPassword", errorsConfirmPassword.ToArray());

      if (errorsNew.Count > 0) throw new ModelValidationException(400, errorsNew);
    }

    bool IsValidEmail(string email)
    {
      try
      {
        var trimmedEmail = email.Trim();

        if (trimmedEmail.EndsWith("."))
        {
          return false;
        }
        var addr = new System.Net.Mail.MailAddress(email);
        return addr.Address == trimmedEmail;
      }
      catch
      {
        return false;
      }
    }

  }
}
