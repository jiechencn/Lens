using Me.JieChen.Lens.Api.Options;
using Me.JieChen.Lens.Api.Utility;
using Me.JieChen.Lens.Extensions;
using Microsoft.AspNetCore.Authentication;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Me.JieChen.Lens.Api.Auth;

/// <summary>
/// Support two kinds of authenN
/// App only (client_credential)
/// User+App (auth code flow or +PKCE)
/// </summary>
class ClientClaimsTransformation : IClaimsTransformation
{
    private readonly AppOptions appOptions;

    public ClientClaimsTransformation(AppOptions appOptions)
    {
        this.appOptions = appOptions;
    }
    public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        var clientType = GetClientType(principal);

        if (clientType == AuthClientType.App.ToDescription())
        {
            //app-only
            AssignClaimsToClientApp(principal);
        }
        else
        {
            //User+App (auth code flow or +PKCE)
            AssignClaimsToClientUser(principal);
        }

        return Task.FromResult(principal);
    }

    private void AssignClaimsToClientUser(ClaimsPrincipal principal)
    {
        /*
         * Scope: On Azure > App Registrations > LensApiApp, defined all scopes, and assigned client application with specified scopes
         * Role: Defined Lens.User and Lens.Admin App roles, then in Enterprise App > add Group and assign role to this Group. Ensure each group has different role
         * So if user logons through client app (auth_flow), Roles and Scp show both in access_token
         * So, no need to customize roles or scopes.
        */
    }

    private void AssignClaimsToClientApp(ClaimsPrincipal principal)
    {
        // Because there is neither Roles nor Scp claims in App-only access_token
        // So we need to customize them in appsettings.json
        var appId = principal.FindFirstValue(AuthClaimType.AZP.ToDescription());
        if (string.IsNullOrEmpty(appId))
        {
            appId = principal.FindFirstValue(AuthClaimType.AppID.ToDescription());
        }

        if (!string.IsNullOrEmpty(appId))
        {
            var identity = principal.Identities.First();

            if (principal.Identity?.Name == null)
            {
                identity.AddClaim(new Claim(AuthClaimType.Name.ToDescription(), appId));
            }

            var hasCustomClaims = appOptions.AuthorizedApps.TryGetValue(appId, out var customClaims);
            if (hasCustomClaims)
            {
                customClaims!.Roles.Distinct().ToList().ForEach(r =>
                {
                    identity.AddClaim(new Claim(AuthClaimType.Roles.ToDescription(), r));
                });


                var scopes = string.Join(CommonConstants.WHITESPACE, customClaims.Scopes.Distinct());
                identity.AddClaim(new Claim(AuthClaimType.Scope.ToDescription(), scopes));
            }
        }
    }

    private string? GetClientType(ClaimsPrincipal principal)
    {
        var clientType =  principal.Claims.Where(c => c.Type.Equals(AuthClaimType.IDType.ToDescription())).FirstOrDefault()?.Value;
        return clientType;
    }
}
