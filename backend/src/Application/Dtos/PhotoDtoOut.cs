
namespace Application.Dtos
{
  public class PhotoDtoOut
  {
    public string Image { get; set; }
    public string Title { get; set; }
    public List<string> Likes { get; set; }
    public List<string> Comments { get; set; }
    public string UserId { get; set; }
    public string UserName { get; set; }
  }
}
