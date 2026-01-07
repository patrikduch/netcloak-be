namespace NetCloak.API.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetCloak.Application.Dtos.Auth.Requests;
using NetCloak.Application.Interfaces.Infrastructure;
using Swashbuckle.AspNetCore.Annotations;

/// <summary>
/// Handles authentication operations using Keycloak identity provider.
/// </summary>
[ApiController]
[Tags("Auth")]
[SwaggerTag("Handles authentication operations using Keycloak identity provider. Provides endpoints for login, token refresh, and user information retrieval.")]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthController"/> class.
    /// </summary>
    /// <param name="authService">The authentication service for Keycloak operations.</param>
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>
    /// Authenticate user with username and password via Keycloak.
    /// </summary>
    /// <param name="request">The login request containing username and password.</param>
    /// <returns>Returns an IActionResult with JWT tokens or unauthorized status.</returns>
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDTO request)
    {
        var result = await _authService.LoginAsync(request);

        return result is null 
            ? Unauthorized("Invalid credentials") 
            : Ok(result);
    }

    /// <summary>
    /// Refresh access token using a valid refresh token.
    /// </summary>
    /// <param name="request">The refresh token request containing the refresh token.</param>
    /// <returns>Returns an IActionResult with new JWT tokens or unauthorized status.</returns>
    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequestDTO request)
    {
        var result = await _authService.RefreshTokenAsync(request.RefreshToken);

        return result is null 
            ? Unauthorized("Invalid refresh token") 
            : Ok(result);
    }

    /// <summary>
    /// Get currently authenticated user.
    /// </summary>
    /// <returns>Returns an IActionResult with list of user claims.</returns>
    [Authorize]
    [HttpGet("me")]
    public IActionResult Me()
    {
        var claims = User.Claims.Select(c => new { c.Type, c.Value });
        return Ok(claims);
    }

    /// <summary>
    /// Protected endpoint for testing JWT authentication.
    /// </summary>
    /// <returns>Returns an IActionResult indicating successful authentication.</returns>
    [Authorize]
    [HttpGet("protected")]
    public IActionResult Protected()
    {
        return Ok("Protected");
    }
}