
namespace Application.Dtos
{
  public class PhotoDtoOut
  {
    public string Id { get; set; }
    public string Image { get; set; }
    public string Title { get; set; }
    public List<string> Likes { get; set; }
    public List<dynamic> Comments { get; set; }
    public string UserId { get; set; }
    public string UserName { get; set; }
  }
}
