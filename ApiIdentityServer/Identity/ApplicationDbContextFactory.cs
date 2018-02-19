using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ApiIdentityServer.Identity
{
  public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
  {
    public static string ConnectionString { get; } =
      @"Data Source=(LocalDb)\MSSQLLocalDB;database=Test.IdentityServer4.EntityFramework2;trusted_connection=yes;";
    public ApplicationDbContext CreateDbContext(string[] args)
    {
      var migrationsAssembly = typeof(ApplicationDbContextFactory).GetTypeInfo().Assembly.GetName().Name;

      var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
      optionsBuilder.UseSqlServer(ConnectionString, sqlOptions => sqlOptions.MigrationsAssembly(migrationsAssembly));

      return new ApplicationDbContext(optionsBuilder.Options);
    }
  }
}
