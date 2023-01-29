using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos;

public class PermissionDto
{
    public int PermissionId { get; set; }
    [Required, MaxLength(50)]
    public string Name { get; set; }
}