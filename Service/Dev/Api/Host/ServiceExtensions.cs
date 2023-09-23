using Me.JieChen.Lens.Api.Dameon;
using Me.JieChen.Lens.Api.Options;
using Me.JieChen.Lens.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace Me.JieChen.Lens.Api.Host;

static class ServiceExtensions
{
    public static void AddCustomServices(this IServiceCollection services, AppOptions appOptions)
    {
        services.AddSingleton(appOptions);
        services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));
        services.AddHostedService( (c) =>
        {
            return new ActivityAuditHostedService(appOptions.Dameon.ActivityAudit, c.GetRequiredService<ILogger<ActivityAuditHostedService>>());
        });

    }
}
