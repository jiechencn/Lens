using System.Collections.Generic;

namespace Me.JieChen.Lens.Api.Options;

class AppClaimsOptions
{
    public List<string> Roles { get; } = new List<string>();
    public List<string> Scopes { get; } = new List<string>();
}
