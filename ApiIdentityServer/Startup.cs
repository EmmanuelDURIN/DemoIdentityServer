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

namespace ApiIdentityServer
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddIdentityServer()
      .AddInMemoryIdentityResources(Resources.GetIdentityResources())
      .AddInMemoryApiResources(Resources.GetApiResources())
      .AddTestUsers(Users.Get())
      .AddInMemoryClients(Clients.Get())
      // on peut utiliser un certificate auto signé, il est exposé par le point de 
      // terminaison de découverte : à l’adresse indiquée par jwks_uri
      .AddDeveloperSigningCredential();

      services.AddMvc();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
    {
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
