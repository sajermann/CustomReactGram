using Application.Helpers;

namespace Application.Dtos
{
  public class UserLogin
  {
    public string Email { get; set; }
    public string Password { get; set; }

    public void IsValid()
    {
      var errorsNew = new Dictionary<string, string[]>();

      List<string> errorsEmails = new List<string>();
      if (Email != null && !IsValidEmail(Email)) errorsEmails.Add("Inválido");
      if (Email == null) errorsEmails.Add("É obrigatório.");
      if (errorsEmails.Count > 0) errorsNew.Add("Email", errorsEmails.ToArray());

      List<string> errorsPassword = new List<string>();
      if (Password == null) errorsPassword.Add("É obrigatório.");
      if (errorsPassword.Count > 0) errorsNew.Add("Password", errorsPassword.ToArray());

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
