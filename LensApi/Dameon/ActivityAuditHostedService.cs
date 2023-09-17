using Me.JieChen.Lens.Api.Options;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Me.JieChen.Lens.Api.Dameon;

class ActivityAuditHostedService : IHostedService, IDisposable
{
    private ActivityAuditOptions options;
    private Timer? realtimeTimer = null;
    private Timer? historicalTimer = null;
    public ActivityAuditHostedService(ActivityAuditOptions options)
    {
        this.options = options;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        realtimeTimer = new Timer(DoRealTimeAudit, null, TimeSpan.Zero, TimeSpan.FromSeconds(options.RealTimeIntervalInSecond));
        historicalTimer = new Timer(DoHistoricalAudit, null, TimeSpan.Zero, TimeSpan.FromSeconds(options.HistoricalIntervalInSecond));

        return Task.CompletedTask;
    }

    private void DoRealTimeAudit(object? state)
    {
        Console.WriteLine(nameof(DoRealTimeAudit));
    }

    private void DoHistoricalAudit(object? state)
    {
        Console.WriteLine(nameof(DoHistoricalAudit));
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
