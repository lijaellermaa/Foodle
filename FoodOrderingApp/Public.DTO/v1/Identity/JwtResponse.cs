﻿namespace Public.DTO.v1.Identity;

public class JwtResponse
{
    public string Token { get; set; } = default!;
    public string RefreshToken { get; set; } = default!;
    public DateTime ExpiresAt { get; set; } = default!;
    public User User { get; set; } = default!;
    public IList<string> Roles { get; set; } = default!;
}