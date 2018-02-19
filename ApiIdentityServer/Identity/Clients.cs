using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiIdentityServer.Identity
{
  internal class Clients
  {
    public static IEnumerable<Client> Get()
    {
      return new List<Client>
    {
        new Client {
            ClientId = "oauthClient",
            ClientName = "Example Client Credentials Client Application",
            AllowedGrantTypes = GrantTypes.ClientCredentials,
            ClientSecrets = new List<Secret> {
                new Secret("p@$$w0rd".Sha256())},
            AllowedScopes = new List<string> { Resources.CourseApiRead }
        },
        new Client {
          ClientId = "openIdConnectClient",
          ClientName = "Example Implicit Client Application",
          AllowedGrantTypes = GrantTypes.Implicit,
          AllowedScopes = new List<string>
          {
              IdentityServerConstants.StandardScopes.OpenId,
              IdentityServerConstants.StandardScopes.Profile,
              IdentityServerConstants.StandardScopes.Email,
              "role",
              Resources.CourseApiWrite,
              Resources.CourseApiRead
          },
          // Url de redirection dans l'application cliente
          // A mettre à jour plus tard avec l’adresse du client
          // Il peut avoir plusieurs Url par client. 
          // Mais la redirection demandée par le login ne sera faite que vers une url de cette liste
          //RedirectUris = new List<string> {"http://localhost:44362/signin-oidc"},
          //PostLogoutRedirectUris = new List<string> {"http://localhost:44362"}
          RedirectUris = new List<string> {"https://localhost:44315/signin-oidc"},
          PostLogoutRedirectUris = new List<string> {"https://localhost:44315"}
        }
    };
    }
  }
}
