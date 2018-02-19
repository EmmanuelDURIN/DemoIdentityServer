using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ApiIdentityServer.Identity;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace ApiIdentityServer
{
  public class Startup
  {
    public const string connectionString =
    @"Data Source=(LocalDb)\MSSQLLocalDB;database=Test.IdentityServer4.EntityFramework2;trusted_connection=yes;";

    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
      services.AddDbContext<ApplicationDbContext>
        (builder =>
          builder.UseSqlServer(connectionString,
                               sqlOptions => sqlOptions.MigrationsAssembly(migrationsAssembly)
          )
        );
      services.AddIdentity<IdentityUser, IdentityRole>()
        .AddEntityFrameworkStores<ApplicationDbContext>();
      services.AddIdentityServer()
      //.AddInMemoryIdentityResources(Resources.GetIdentityResources())
      //.AddInMemoryApiResources(Resources.GetApiResources())
      //.AddInMemoryClients(Clients.Get())
      //.AddTestUsers(Users.Get())
     
      // Client and scope stores
      .AddConfigurationStore(options => options.ConfigureDbContext = builder =>
            builder.UseSqlServer(connectionString,
                                 sqlOptions => sqlOptions.MigrationsAssembly(migrationsAssembly)))
      // JWT Store
      .AddOperationalStore
        (
          options => options.ConfigureDbContext = builder =>
          builder.UseSqlServer(connectionString, sqlOptions => sqlOptions.MigrationsAssembly(migrationsAssembly))
        )
      .AddAspNetIdentity<IdentityUser>()
      // on peut utiliser un certificate auto signé, il est exposé par le point de 
      // terminaison de découverte : à l’adresse indiquée par jwks_uri
      .AddDeveloperSigningCredential();

      services.AddMvc();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
    {
      app.InitializeDbTestData();
      loggerFactory.AddConsole(LogLevel.Trace);
      loggerFactory.AddDebug(LogLevel.Trace);
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      app.UseIdentityServer();
      app.UseStaticFiles();
      app.UseMvcWithDefaultRoute();
    }
  }
}
