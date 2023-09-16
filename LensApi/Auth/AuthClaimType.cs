using System.ComponentModel;

namespace Me.JieChen.Lens.Api.Auth;

/// <summary>
/// Claims type. The description must be exactly the same with OAuth 1.x/2.x claim string
/// </summary>
enum AuthClaimType
{
    [Description("appid")]
    AppID,

    [Description("azp")]
    AZP,

    [Description("idtyp")]
    IDType,

    [Description("name")]
    Name,

    [Description("http://schemas.microsoft.com/identity/claims/objectidentifier")]
    OID,

    [Description("http://schemas.microsoft.com/ws/2008/06/identity/claims/role")]
    Roles,

    [Description("scp")]
    Scope
}
