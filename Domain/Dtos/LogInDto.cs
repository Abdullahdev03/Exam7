using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos;

public class LogInDto
{
    
    [Required]
    public string Username { get; set; }
    [Required,DataType(DataType.Password)]
    public string Password { get; set; }
}