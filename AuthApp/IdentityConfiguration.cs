using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace AuthApp
{
    public static class IdentityConfiguration
    {
        public static IEnumerable<ApiScope> ApiScopes => new List<ApiScope>()
        {
            new ApiScope ("SportStoreWebApi","Web API")
        };

        public static IEnumerable<IdentityResource> IdentityResources => new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile()
        };

        public static IEnumerable<ApiResource> ApiResources => new List<ApiResource>
        {
            new ApiResource("SportStoreWebApi","Web API",new [] {JwtClaimTypes.Name })
            {
                Scopes={ "SportStoreWebApi" }
            }
        };

        public static IEnumerable<Client> Clients => new List<Client>
        {
            new Client
            {
                ClientId = "SportStore-web-app",
                ClientName = "SportStore Web",
                AllowedGrantTypes = GrantTypes.Code,
                RequireClientSecret = false,
                RequirePkce = true,
                RedirectUris =
                {
                    "http://.../signin-oidc"
                },
                AllowedCorsOrigins =
                {
                    "http://..."
                },
                PostLogoutRedirectUris =
                {
                    "http://.../signout-oidc"
                },
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "SportStoreWebApi"
                },
                AllowAccessTokensViaBrowser = true
            }
        };
    }
}
