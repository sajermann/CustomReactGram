namespace Application.Helpers
{
  public static class Password
  {
    public static string Encripty(string pass)
    {
      int workfactor = 4;
      string salt = BCrypt.Net.BCrypt.GenerateSalt(workfactor);
      string hash = BCrypt.Net.BCrypt.HashPassword(pass, salt);

      return hash;
    }

    public static bool VerifyPass(string pass, string hash)
    {
      return BCrypt.Net.BCrypt.Verify(pass, hash);
    }
  }
}
