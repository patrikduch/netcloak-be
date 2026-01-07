namespace NetCloak.Persistence.Diagnostics;

using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

public class EfCoreDiagnosticListener : IObserver<DiagnosticListener>, IObserver<KeyValuePair<string, object>>
{
    private readonly Stopwatch _stopwatch = new();

    public void OnNext(DiagnosticListener listener)
    {
        if (listener.Name == DbLoggerCategory.Name)
        {
            listener.Subscribe(this);
        }
    }

    public void OnNext(KeyValuePair<string, object> value)
    {
        if (value.Key == RelationalEventId.CommandExecuting.Name)
        {
            _stopwatch.Restart();
        }
        else if (value.Key == RelationalEventId.CommandExecuted.Name)
        {
            _stopwatch.Stop();
            var eventData = (CommandExecutedEventData)value.Value;
            LogIfSlow(eventData.Command.CommandText, _stopwatch.Elapsed);
        }
    }

    public void OnCompleted()
    {

    }

    public void OnError(Exception error) { }

    private void LogIfSlow(string query, TimeSpan elapsed)
    {
        const int slowQueryThresholdMs = 1000; // Set your threshold (e.g., 1000ms)
        if (elapsed.TotalMilliseconds > slowQueryThresholdMs)
        {
            Console.WriteLine($"[SLOW QUERY] Execution Time: {elapsed.TotalMilliseconds}ms\nQuery: {query}");
        }
    }
}