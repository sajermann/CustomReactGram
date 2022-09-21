namespace Domain
{
  public record Photo: BaseEntity
  {
    public string Image { get; set; }
    public string Title { get; set; }
    public List<string> Likes { get; set; }
    public List<string> Comments { get; set; }
    public Guid userId { get; set; }
    public string Username { get; set; }
  }
}