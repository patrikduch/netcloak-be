namespace NetCloak.Application.Dtos.Auth.Responses;

public class TokenResponseDTO
{
    public TokenResponseDTO(string accessToken, string refreshToken, int expiresIn, string tokenType)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
        ExpiresIn = expiresIn;
        TokenType = tokenType;
    }

    public TokenResponseDTO() { }

    public string AccessToken { get; set; } = string.Empty;

    public string RefreshToken { get; set; } = string.Empty;

    public int ExpiresIn { get; set; }

    public string TokenType { get; set; } = string.Empty;
}