namespace NetCloak.Infrastructure.Services;

using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using NetCloak.Application.Dtos.Auth.Requests;
using NetCloak.Application.Dtos.Auth.Responses;
using NetCloak.Application.Interfaces.Infrastructure;

public class KeycloakAuthService : IAuthService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public KeycloakAuthService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    public async Task<TokenResponseDTO?> LoginAsync(LoginRequestDTO request)
    {
        var tokenEndpoint = $"{_configuration["Keycloak:Authority"]}/protocol/openid-connect/token";

        var content = new FormUrlEncodedContent(new Dictionary<string, string>
        {
            ["grant_type"] = "password",
            ["client_id"] = _configuration["Keycloak:ClientId"]!,
            ["client_secret"] = _configuration["Keycloak:ClientSecret"]!,
            ["username"] = request.Username,
            ["password"] = request.Password,
        });

        var response = await _httpClient.PostAsync(tokenEndpoint, content);

        if (!response.IsSuccessStatusCode)
            return null;

        var json = await response.Content.ReadFromJsonAsync<JsonElement>();

        return new TokenResponseDTO(
            json.GetProperty("access_token").GetString() !,
            json.GetProperty("refresh_token").GetString() !,
            json.GetProperty("expires_in").GetInt32(),
            json.GetProperty("token_type").GetString() !);
    }

    public async Task<TokenResponseDTO?> RefreshTokenAsync(string refreshToken)
    {
        var tokenEndpoint = $"{_configuration["Keycloak:Authority"]}/protocol/openid-connect/token";

        var content = new FormUrlEncodedContent(new Dictionary<string, string>
        {
            ["grant_type"] = "refresh_token",
            ["client_id"] = _configuration["Keycloak:ClientId"]!,
            ["client_secret"] = _configuration["Keycloak:ClientSecret"]!,
            ["refresh_token"] = refreshToken,
        });

        var response = await _httpClient.PostAsync(tokenEndpoint, content);

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var json = await response.Content.ReadFromJsonAsync<JsonElement>();

        return new TokenResponseDTO(
            json.GetProperty("access_token").GetString() !,
            json.GetProperty("refresh_token").GetString() !,
            json.GetProperty("expires_in").GetInt32(),
            json.GetProperty("token_type").GetString() !);
    }
}