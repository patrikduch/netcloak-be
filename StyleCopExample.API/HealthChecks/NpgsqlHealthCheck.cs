namespace StyleCop.HealthChecks;

using Microsoft.Extensions.Diagnostics.HealthChecks;
using Npgsql;


/// <summary>
/// Represents a custom health check for Npgsql database connections.
/// Implements the <see cref="IHealthCheck"/> interface.
/// </summary>
public class NpgsqlHealthCheck(string connectionString): IHealthCheck
{
    /// <summary>
    /// Performs the health check operation asynchronously.
    /// </summary>
    /// <param name="context">The context in which the health check is being performed.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the health check operation.</param>
    /// <returns>
    /// A task that represents the asynchronous health check operation. 
    /// The task result contains a <see cref="HealthCheckResult"/> indicating the health of the database connection.
    /// </returns>
    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        try
        {
            await using var connection = new NpgsqlConnection(connectionString);
            await connection.OpenAsync(cancellationToken);

            if (connection.State == System.Data.ConnectionState.Open)
            {
                return HealthCheckResult.Healthy("Database connection is healthy.");
            }

            return HealthCheckResult.Unhealthy("Database connection is not healthy.");
        }
        catch (Exception ex)
        {
            return HealthCheckResult.Unhealthy("Database connection is not healthy.", ex);
        }
    }
}