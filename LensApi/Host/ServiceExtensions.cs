using Me.JieChen.Lens.Api.Options;
using Microsoft.Extensions.DependencyInjection;

namespace Me.JieChen.Lens.Api.Host;

static class ServiceExtensions
{
    public static void AddCustomServices(this IServiceCollection services, AppOptions appOptions)
    {
        services.AddSingleton(appOptions);
    }
}
