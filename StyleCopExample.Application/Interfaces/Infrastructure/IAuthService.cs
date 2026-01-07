namespace NetCloak.Application.Interfaces.Infrastructure;

using NetCloak.Application.Dtos.Auth.Requests;
using NetCloak.Application.Dtos.Auth.Responses;

public interface IAuthService
{
    Task<TokenResponseDTO?> LoginAsync(LoginRequestDTO request);

    Task<TokenResponseDTO?> RefreshTokenAsync(string refreshToken);
}