using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiIdentityServer.Identity
{
  internal class Resources
  {
    internal const string CourseApiRead = "courseAPI.read";
    internal const string CourseApiWrite = "courseAPI.write";

    internal static IEnumerable<IdentityResource> GetIdentityResources()
    {
      return new List<IdentityResource> {
          // 3 Ressources standard OpenId
          // Le scope OpenId est toujours requis
          new IdentityResources.OpenId(),
          new IdentityResources.Profile(),
          // Pour accéder aux claims standard email et email_verified
          new IdentityResources.Email(),
          // Ressource propre à l'application
          new IdentityResource {
              Name = "role",
              UserClaims = new List<string> {"role"}
          }
     };
    }
    internal static IEnumerable<ApiResource> GetApiResources()
    {
      return new List<ApiResource> {
          new ApiResource {
              Name = "courseAPI",
              DisplayName = "Course API",
              Description = "Course API Access",
              UserClaims = new List<string> {"role"},
              // la sécurisation des ressources par mdp sert à éviter la vérification des tokens en libre service
              // seuls les clients possesseurs de API peuvent vérifier le scope dans un token
              ApiSecrets = new List<Secret> {new Secret("sc0pe$ecret".Sha256())},
              Scopes = new List<Scope> {
                new Scope(CourseApiRead),
                new Scope(CourseApiWrite)
              }
          }
      };
    }
  }

}
