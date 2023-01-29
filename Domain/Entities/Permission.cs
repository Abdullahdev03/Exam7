using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Permission
{
    
    public int PermissionId { get; set; }
    [Required, MaxLength(50)]
    public string Name { get; set; }
    public  ICollection<RolePermission> RolePermissions { get; set; }

}
