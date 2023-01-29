namespace Domain.Dtos;

public class UserLoginDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateTime LogInDate { get; set; }
    public DateTime LogOutDate { get; set; }
}