namespace Domain
{
  public record User: BaseEntity
  {
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string ProfileImage { get; set; }
    public string Bio { get; set; }
  }
}