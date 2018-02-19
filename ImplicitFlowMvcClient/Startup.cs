using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ImplicitFlowMvcClient
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
      services.AddMvc();
      services.AddAuthentication(options =>
      {
        options.DefaultScheme = "cookie";
        options.DefaultChallengeScheme = "oidc";
      })
              .AddCookie("cookie")
              .AddOpenIdConnect("oidc", options =>
              {
                // Si https ne peut pas être activé :
                //options.RequireHttpsMetadata = false;
                //options.Authority = "http://localhost:44329/";
                options.Authority = "https://localhost:44397/";
                options.ClientId = "openIdConnectClient";
                options.SignInScheme = "cookie";
              });

    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        // Pour clarifier les traces réseau
        //app.UseBrowserLink();
      }
      else
      {
        app.UseExceptionHandler("/Home/Error");
      }

      app.UseStaticFiles();
      app.UseAuthentication();
      app.UseMvc(routes =>
      {
        routes.MapRoute(
                  name: "default",
                  template: "{controller=Home}/{action=Index}/{id?}");
      });
    }
  }
}
