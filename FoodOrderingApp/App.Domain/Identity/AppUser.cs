using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace App.Domain.Identity;

public class AppUser : IdentityUser<Guid>
{
    [MaxLength(128)]
    public string FirstName { get; set; } = default!;
    
    [MaxLength(128)]
    public string LastName { get; set; } = default!;

    [MaxLength(256)]
    public string Address { get; set; } = default!;
    
    public ICollection<Order>? Orders { get; set; }
    public ICollection<AppRefreshToken>? AppRefreshTokens { get; set; }
}