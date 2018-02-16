using IdentityModel;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ApiIdentityServer.Identity
{
  internal class Users
  {
    internal static List<TestUser> Get()
    {
      return new List<TestUser> {
          new TestUser {
              SubjectId = "4B596575-5ED5-484F-9ED9-81E179FB6771",
              Username = "emmanuel",
              Password = "P@ssw0rd",
              Claims = new List<Claim> {
                  new Claim(JwtClaimTypes.Email, "emmanuel@durin.org"),
                  new Claim(JwtClaimTypes.Role, "admin")
              }
          }
      };
    }
  }
}
