using System.ComponentModel.DataAnnotations;

public class UpdateStaffDto
{
    [Required]
    [MaxLength(250)]
    public string Name {get; set;} = default!;

    [EmailAddress]
    [MaxLength(250)]
    public string? Email {get; set;}
}