namespace Application.Dtos
{
  public record UserDtoOut : BaseEntity
  {
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string ProfileImage { get; set; }
    public string Bio { get; set; }
    public string Jwt { get; set; }

  }
}
