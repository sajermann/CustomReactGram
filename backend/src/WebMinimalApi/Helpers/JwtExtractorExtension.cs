namespace WebMinimalApi.Helpers
{
  public static class JwtExtractorExtension
  {

    public static string JwtExtractor(this HttpRequest request)
    {
      return request.Headers["Authorization"].ToString().Replace("Bearer ", "");
    }
  }
}
