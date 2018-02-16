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
        }
    };
    }
  }
}
