namespace NetCloak.Application.Dtos.Auth.Responses;

public class LoginResponseDTO
{

    public LoginResponseDTO()
    {
        
    }

    public string AccessToken { get; set; } = string.Empty;

    public string RefreshToken { get; set; } = string.Empty;

    public int ExpiresIn { get; set; }

    public string TokenType { get; set; } = string.Empty;
}
