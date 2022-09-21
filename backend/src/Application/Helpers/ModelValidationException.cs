namespace Application.Helpers
{
  public class ModelValidationException : Exception
  {
    public int StatusCode { get; }
    public IDictionary<string, string[]> Errors { get; }
    public ModelValidationException(int StatusCode, IDictionary<string, string[]> Errors)
    {
      this.StatusCode = StatusCode;
      this.Errors = Errors;
    }
  }
}
