using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos;

public class UserRoleDto
{
    
    public int Id { get; set; }
    public int UserId { get; set; }
    [Required, MaxLength(50)]
    public string UserName { get; set; }
    public int RoleId { get; set; }
    [Required, MaxLength(50)]
    public string RoleName { get; set; }
}