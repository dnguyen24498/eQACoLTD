using IdentityModel;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace eQACoLTD.IdentityServer.Configurations
{
    public static class IdentityServerConfig
    {
        public static IEnumerable<ApiResource> GetApiResources() => new List<ApiResource>
        {
            new ApiResource("backend_api","Backend API")
        };

        public static IEnumerable<IdentityResource> GetIdentityResources() => new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResource("roles","User role(s)",new List<string>{"role"})
        };

        public static IEnumerable<Client> GetClients() => new List<Client>
        {
            new Client()
            {
                ClientId="mvc_client",
                ClientSecrets={new Secret("secret_key_mvc".ToSha256())},
                AllowedGrantTypes=GrantTypes.Code,
                RequireConsent=false,
                RequirePkce=true,
                RedirectUris={ "https://localhost:5003/signin-oidc" },
                PostLogoutRedirectUris={ "https://localhost:5003/signout-callback-oidc" },
                AllowedScopes =
                {
                    IdentityServer4.IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServer4.IdentityServerConstants.StandardScopes.Profile,
                    "backend_api",
                    "roles"
                },
                AllowOfflineAccess=true,
                UpdateAccessTokenClaimsOnRefresh=true,
            },
            new Client()
            {
                ClientId="mvc_admin",
                ClientSecrets={new Secret("secret_key_mvc".ToSha256())},
                AllowedGrantTypes=GrantTypes.Code,
                RequireConsent=false,
                RequirePkce=true,
                RedirectUris={ "https://localhost:5002/signin-oidc" },
                PostLogoutRedirectUris={ "https://localhost:5002/signout-callback-oidc" },
                AllowedScopes =
                {
                    IdentityServer4.IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServer4.IdentityServerConstants.StandardScopes.Profile,
                    IdentityServer4.IdentityServerConstants.StandardScopes.OfflineAccess,
                    "backend_api",
                    "roles"
                },
                AllowOfflineAccess=true,
                UpdateAccessTokenClaimsOnRefresh=true,
            },
            new Client
                {
                    ClientId = "backend_api_swagger",
                    ClientName = "Swagger UI for demo_api",
                    ClientSecrets = {new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireConsent=false,
                    RequireClientSecret = false,
                    RedirectUris = {"https://localhost:5001/swagger/oauth2-redirect.html"},
                    AllowedCorsOrigins = {"https://localhost:5001"},
                    AllowedScopes = {"backend_api","roles"}
                }
        };
    }
}
