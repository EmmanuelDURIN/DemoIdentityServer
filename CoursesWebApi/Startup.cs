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

namespace CoursesWebApi
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
      services.AddMvcCore()
              .AddAuthorization
              (
                options => {
                  // require courseAPI.write
                  options.AddPolicy("courseWritePolicy", builder =>
                  {
                    builder.RequireClaim("scope", "courseAPI.write");
                  });
                }
              )
              .AddJsonFormatters();

      services.AddAuthentication("Bearer")
              .AddJwtBearer(options =>
              {
                // base-address of your identityserver
                options.Authority = "https://localhost:44397";

                // name of the API resource
                options.Audience = "courseAPI";
              });

      services.AddCors(options =>
      {
        // this defines a CORS policy called "default"
        options.AddPolicy("default", policy =>
        {
          policy.WithOrigins("https://localhost:44340")
              .AllowAnyHeader()
              .AllowAnyMethod();
        });
      });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      app.UseCors("default");

      app.UseAuthentication();

      app.UseMvc();
    }
  }
}
