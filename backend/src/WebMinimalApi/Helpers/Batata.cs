using GoBarber.Api.Helpers.Interfaces;

namespace GoBarber.Api.Helpers
{
  public class Batata : IBatata
  {
    public void Error()
    {
      var t = DateTime.Now;
    }
  }
}
