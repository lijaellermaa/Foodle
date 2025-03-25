namespace Public.DTO.v1.Identity;

public class RefreshTokenModel
{
    public string Token { get; set; } = default!;
    public string RefreshToken { get; set; } = default!;
}