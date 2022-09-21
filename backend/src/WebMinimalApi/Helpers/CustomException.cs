using Api.Helpers.Interfaces;

namespace Api.Helpers
{
  public class CustomException : ICustomException
  {
    public IResult Error(string error)
    {
      IDictionary<string, int> errors = new Dictionary<string, int>()
      {
        {"Id da aplicação não localizado!", 400 },
        {"Id não localizado!", 400 },
        {"Id do usuário pai não localizado!", 400 },
        {"Incorrect Email/Pass combination.", 400 },
        {"Você não tem permissão para essa ação!", 403 },
        {"Senha incorreta!", 400 },
        {"Senha atual incorreta!", 400 },
        {"Novas senhas não idênticas!", 400 },
        {"Username informado já encontra-se em uso!", 400 },
        {"Jwt is missing!", 500 },
      };
      var statusCode = 0;
      errors.TryGetValue(error, out statusCode);
      
      if (statusCode != 0)
      {
        return Results.Problem($"{error}", null, statusCode);
      }
      return Results.Problem($"{error}", null, 500);
    }
  }
}
