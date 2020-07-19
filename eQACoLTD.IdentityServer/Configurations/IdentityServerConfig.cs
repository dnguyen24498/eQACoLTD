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
            new ApiResource("backend_api","Backend API",new List<string>(){ ClaimTypes.Role}),
        };

        public static IEnumerable<IdentityResource> GetIdentityResources() => new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResource()
            {
                Name="roles",
                UserClaims =
                {
                    ClaimTypes.Role
                }
            }
        };

        public static IEnumerable<Client> GetClients() => new List<Client>
        {
            new Client()
            {
                ClientId="mvc_client",
                ClientSecrets={new Secret("secret_key_mvc".ToSha256())},
                AllowedGrantTypes=GrantTypes.Code,
                RequireConsent=false,
                RedirectUris={ "https://localhost:5003/signin-oidc" },
                PostLogoutRedirectUris={ "https://localhost:5003/Home/Index" },
                AllowedScopes =
                {
                    IdentityServer4.IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServer4.IdentityServerConstants.StandardScopes.Profile,
                    "backend_api",
                    "roles"
                }
            },
            new Client()
            {
                ClientId="mvc_admin",
                ClientSecrets={new Secret("secret_key_mvc".ToSha256())},
                AllowedGrantTypes=GrantTypes.Code,
                RequireConsent=false,
                RedirectUris={ "https://localhost:5002/signin-oidc" },
                PostLogoutRedirectUris={ "https://localhost:5002/Home/Index" },
                AllowedScopes =
                {
                    IdentityServer4.IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServer4.IdentityServerConstants.StandardScopes.Profile,
                    "backend_api",
                    "roles"
                }
            }
        };
    }
}
