using Me.JieChen.Lens.Api.Options;
using Me.JieChen.Lens.Logging;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Me.JieChen.Lens.Api.Dameon;

class ActivityAuditHostedService : IHostedService, IDisposable
{
    private readonly ILogger<ActivityAuditHostedService> logger;
    private ActivityAuditOptions options;
    private Timer? realtimeTimer = null;
    private Timer? historicalTimer = null;
    public ActivityAuditHostedService(ActivityAuditOptions options, ILogger<ActivityAuditHostedService> logger)
    {
        this.options = options;
        this.logger = logger;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        realtimeTimer = new Timer(DoRealTimeAudit, null, TimeSpan.Zero, TimeSpan.FromSeconds(options.RealTimeIntervalInSecond));
        historicalTimer = new Timer(DoHistoricalAudit, null, TimeSpan.Zero, TimeSpan.FromSeconds(options.HistoricalIntervalInSecond));

        return Task.CompletedTask;
    }

    private void DoRealTimeAudit(object? state)
    {
        logger.LogDebug(nameof(DoRealTimeAudit));
    }

    private void DoHistoricalAudit(object? state)
    {
        logger.LogDebug(nameof(DoHistoricalAudit));
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        realtimeTimer?.Dispose();
        historicalTimer?.Dispose();
    }
}
