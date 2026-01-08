namespace NetCloak.Infrastructure.Services;

using System.Text.Json;
using Microsoft.Extensions.Options;
using NetCloak.Application.Dtos.Auth.Requests;
using NetCloak.Application.Dtos.Auth.Responses;
using NetCloak.Application.Interfaces.Infrastructure;
using NetCloak.Infrastructure.Options;

public sealed class KeycloakAuthService : IAuthService
{
    private readonly HttpClient _httpClient;
    private readonly KeycloakOptions _options;

    public KeycloakAuthService(
        HttpClient httpClient,
        IOptions<KeycloakOptions> options)
    {
        _httpClient = httpClient;
        _options = options.Value;
    }

    private string TokenEndpoint =>
        $"{_options.Authority.TrimEnd('/')}/protocol/openid-connect/token";

    public async Task<LoginResponseDTO?> LoginAsync(LoginRequestDTO request)
    {
        using var content = new FormUrlEncodedContent(new Dictionary<string, string>
        {
            ["grant_type"] = "password",
            ["client_id"] = _options.ClientId,
            ["client_secret"] = _options.ClientSecret,
            ["username"] = request.Username,
            ["password"] = request.Password,
        });

        using var response = await _httpClient.PostAsync(TokenEndpoint, content);

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        return await ReadTokenResponseAsync(response);
    }

    public async Task<LoginResponseDTO?> RefreshTokenAsync(string refreshToken)
    {
        using var content = new FormUrlEncodedContent(new Dictionary<string, string>
        {
            ["grant_type"] = "refresh_token",
            ["client_id"] = _options.ClientId,
            ["client_secret"] = _options.ClientSecret,
            ["refresh_token"] = refreshToken,
        });

        using var response = await _httpClient.PostAsync(TokenEndpoint, content);

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        return await ReadTokenResponseAsync(response);
    }

    private static async Task<LoginResponseDTO?> ReadTokenResponseAsync(HttpResponseMessage response)
    {
        await using var stream = await response.Content.ReadAsStreamAsync();

        var tokenResponse = await JsonSerializer.DeserializeAsync<LoginResponseDTO>(
            stream,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        if (tokenResponse is null || string.IsNullOrWhiteSpace(tokenResponse.AccessToken))
        {
            return null;
        }

        return tokenResponse;
    }
}
