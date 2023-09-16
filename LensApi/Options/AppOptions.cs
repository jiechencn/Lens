using System.Collections.Generic;

namespace Me.JieChen.Lens.Api.Options;

class AppOptions
{
    public EnvironmentOptions Environment { get; set; } = new EnvironmentOptions();
    public Dictionary<string, AppClaimsOptions> AuthorizedApps { get; } = new Dictionary<string, AppClaimsOptions>();
}
