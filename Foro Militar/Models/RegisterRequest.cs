using System.ComponentModel.DataAnnotations;

public class RegisterRequest
{
    [Required]
    [MaxLength(50)]
    public string Username { get; set; }

    [Required]
    [MaxLength(100)]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [MinLength(6)]
    public string Password { get; set; }
}