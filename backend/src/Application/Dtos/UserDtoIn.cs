using Application.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Application.Dtos
{
  public class UserDtoIn
  {
    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [MaxLength(20, ErrorMessage = "O tamanho máximo do campo {0} é {1}.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [MaxLength(50, ErrorMessage = "O tamanho máximo do campo {0} é {1}.")]
    public string Password { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [MaxLength(100, ErrorMessage = "O tamanho máximo do campo {0} é {1}.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [MaxLength(250, ErrorMessage = "O tamanho máximo do campo {0} é {1}.")]
    public string Avatar { get; set; }

    public void IsValid()
    {
      int prop1 = 0;
      int prop2 = 0;
      int prop3 = 0;
      int prop4 = 0;
      IDictionary<string, object> errors = new Dictionary<string, object>();

      if (Name != null && Name.Length > 20) errors.Add($"Password__{prop1++}", "Tamanho máximo de 20 caracteres.");
      if (Name == null) errors.Add($"Name__{prop1++}", "É obrigatório.");

      if (Password != null && Password.Length > 50) errors.Add($"Password__{prop2++}", "Tamanho máximo de 50 caracteres.");
      if (Password == null) errors.Add($"Password__{prop2++}", "É obrigatório.");

      if (Email != null && Email.Length > 100) errors.Add($"Password__{prop3++}", "Tamanho máximo de 100 caracteres.");
      if (Email == null) errors.Add($"Email__{prop3++}", "É obrigatório.");     
      
      if (Avatar != null && Avatar.Length > 250) errors.Add($"Password__{prop4++}", "Tamanho máximo de 250 caracteres.");
      if (Avatar == null) errors.Add($"Avatar__{prop4++}", "É obrigatório.");


      //if (errors.Count > 0) throw new ModelValidationException(400, errors);
    }

  }
}
