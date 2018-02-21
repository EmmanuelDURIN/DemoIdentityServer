using IdentityServer4;
using IdentityServer4.EntityFramework.DbContexts;
//using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using static IdentityModel.OidcConstants;

namespace ApiIdentityServer.Identity
{
  public static class InitializeIdentitiesExtension
  {
    public static void CreateSpaClient(this IApplicationBuilder app)
    {
      using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
      {
        Client spaClient = new Client
        {
          ClientId = "js",
          ClientName = "JS Client",
          AllowedGrantTypes = IdentityServer4.Models.GrantTypes.Implicit,
          AllowAccessTokensViaBrowser = true,

          AllowedScopes = new List<string>
          {
              IdentityServerConstants.StandardScopes.OpenId,
              IdentityServerConstants.StandardScopes.Profile,
//              "role",
              //Resources.CourseApiWrite,
              Resources.CourseApiRead
          },
          AllowedCorsOrigins = { "https://localhost:44340" },
          //RedirectUris = { "https://localhost:44340/index.html" },
          //PostLogoutRedirectUris = { "https://localhost:44340/callback.html" }
          RedirectUris = { "https://localhost:44340/callback.html" },
          PostLogoutRedirectUris = { "https://localhost:44340/index.html" }
        };
        using (var context = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>())
        {
          if (!context.Clients.Any(c => c.ClientId == spaClient.ClientId))
          {
            context.Clients.Add(spaClient.ToEntity());
            context.SaveChanges();
          }
        }
      }
    }
    public static void InitializeDbTestData(this IApplicationBuilder app)
    {
      using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
      {
        scope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();
        scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>().Database.Migrate();
        scope.ServiceProvider.GetRequiredService<ApplicationDbContext>().Database.Migrate();

        var context = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();

        if (!context.Clients.Any())
        {
          foreach (var client in Clients.Get())
          {
            context.Clients.Add(client.ToEntity());
          }
          context.SaveChanges();
        }

        if (!context.IdentityResources.Any())
        {
          foreach (var resource in Resources.GetIdentityResources())
          {
            context.IdentityResources.Add(resource.ToEntity());
          }
          context.SaveChanges();
        }

        if (!context.ApiResources.Any())
        {
          foreach (var resource in Resources.GetApiResources())
          {
            context.ApiResources.Add(resource.ToEntity());
          }
          context.SaveChanges();
        }

        CreateUsers(scope);
      }
    }

    private static void CreateUsers(IServiceScope scope)
    {
      var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
      if (!userManager.Users.Any())
      {
        foreach (var testUser in Users.Get())
        {
          var identityUser = new IdentityUser(testUser.Username)
          {
            Id = testUser.SubjectId
          };

          userManager.CreateAsync(identityUser, "P@ssw0rd").Wait();
          userManager.AddClaimsAsync(identityUser, testUser.Claims.ToList()).Wait();
        }
      }
    }
  }
}
