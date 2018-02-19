using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiIdentityServer.Identity
{
  public static class InitializeIdentitiesExtension
  {
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
