using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos;

public class UserDto
{
    
    public int UserId { get; set; }
    [Required, MaxLength(50)]
    public string Name { get; set; }
    [Required,MaxLength(50)]
    [DataType(DataType.EmailAddress)]
    [EmailAddress]
    public string Email { get; set; }
    [Required, MaxLength(20)]
    public string Phone { get; set; }
    [Required(ErrorMessage = "Password is required")]
    [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
    [DataType(DataType.Password)]
    public string Password  { get; set; }
    [Required(ErrorMessage = "Confirm Password is required")]
    [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
    [DataType(DataType.Password)]
    [Compare("Password")]
    public string ConfirmPassword { get; set; }
}