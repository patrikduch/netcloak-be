namespace NetCloak.Infrastructure.Options;

public sealed class KeycloakOptions
{
    public const string SectionName = "Keycloak";

    public string Authority { get; init; } = default!;

    public string ClientId { get; init; } = default!;

    public string ClientSecret { get; init; } = default!;
}