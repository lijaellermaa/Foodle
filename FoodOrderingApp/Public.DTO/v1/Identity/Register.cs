using System.ComponentModel.DataAnnotations;

namespace Public.DTO.v1.Identity;

public class Register
{
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Incorrect length")]
    public string Password { get; set; } = default!;
    
    [StringLength(128, MinimumLength = 1, ErrorMessage = "Incorrect length")]
    public string FirstName { get; set; } = default!;

    [StringLength(128, MinimumLength = 1, ErrorMessage = "Incorrect length")]
    public string LastName { get; set; } = default!;

    [StringLength(256, MinimumLength = 1, ErrorMessage = "Incorrect length")]
    public string Address { get; set; } = default!;
    
    [StringLength(128, MinimumLength = 5, ErrorMessage = "Incorrect length")]
    public string Email { get; set; } = default!;
}