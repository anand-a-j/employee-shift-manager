using System.ComponentModel.DataAnnotations;

public class RegisterDto
{
    [Required]
    [MaxLength(250)]
    public string Name { get; set; } = default!;

    [Required]
    [EmailAddress]
    [MaxLength(250)]
    public string Email { get; set; } = default!;

    [Required]
    [MinLength(6)]
    [MaxLength(100)]
    public string Password { get; set; } = default!;

    [Required]
    [MaxLength(200)]
    public string BusinessName { get; set; } = default!;
}