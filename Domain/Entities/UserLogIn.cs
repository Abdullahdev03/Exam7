namespace Domain.Entities;

public class UserLogIn
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public DateTime LogInDate { get; set; }
    public DateTime LogOutDate { get; set; }
    
}