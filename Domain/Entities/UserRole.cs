namespace Domain.Entities;

public class UserRole
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string UserName { get; set; }
    public User User { get; set; }
    public int RoleId { get; set; }
    public string RoleName { get; set; }
    public Role Role { get; set; }
}