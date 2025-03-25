using System.ComponentModel.DataAnnotations;

namespace Public.DTO.v1.Identity;

public class User
{
    public Guid Id { get; set; }

    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Address { get; set; } = default!;
    public string? Email { get; set; }
}