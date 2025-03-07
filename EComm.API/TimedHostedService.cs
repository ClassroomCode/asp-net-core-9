namespace EComm.API;

public class TimedHostedService : BackgroundService
{
    private readonly IServiceProvider _services;
    private readonly ILogger<TimedHostedService> _logger;
    private static DateTime? _lastSent = null;

    public TimedHostedService(IServiceProvider services, ILogger<TimedHostedService> logger)
    {
        _services = services;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken token)
    {
        _logger.LogInformation("Timed Hosted Service running.");

        using PeriodicTimer timer = new(TimeSpan.FromSeconds(5));

        try
        {
            while (await timer.WaitForNextTickAsync(token))
            {
                DoWork();
            }
        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation("Timed Hosted Service is stopping.");
        }
    }

    private void DoWork()
    {
        _logger.LogInformation("Timed Hosted Service is working");

        _logger.LogCritical("******* FOO ***********");

        _logger.LogInformation("Timed Hosted Service is done working");
    }
}
