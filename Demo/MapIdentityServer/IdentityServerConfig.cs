using System.Collections.Generic;
using System.Linq;
using IdentityServer4.Models;

namespace MapIdentityServer
{
    public class IdentityServerConfig
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("map-api", "Map API")
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials.Union(GrantTypes.ResourceOwnerPassword).ToList(),
                    AllowedScopes =
                    {
                        IdentityServer4.IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServer4.IdentityServerConstants.StandardScopes.Profile,
                        IdentityServer4.IdentityServerConstants.StandardScopes.OfflineAccess,
                        "map-api"
                    },
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    UpdateAccessTokenClaimsOnRefresh = true,
                    AllowOfflineAccess = true
                }
            };
        }
    }
}
