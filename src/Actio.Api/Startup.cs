using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MassTransit;
using Actio.Infraestructure.IOC.Config;
using Actio.Infraestructure.IOC.Mongo;
using Actio.Infraestructure.Config.Mongo;
using Actio.Infraestructure.Config.IOC;
using Actio.Infraestructure.Config.Auth;

namespace Actio.Api
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
      services.AddHealthChecks();

      services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

      services.Configure<RabbitConfig>(Configuration.GetSection("Rabbit"));
      services.Configure<MongoOptions>(Configuration.GetSection("Mongo"));
      services.Configure<JwtOptions>(Configuration.GetSection("jwt"));

      var provider = services.BuildServiceProvider();

      services.AddRabbitMq();
      services.AddMongoDB(provider);
      services.AddActivities();    
      services.AddJwt(provider);
    }


    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
        app.UseHttpsRedirection();
      }

      using (var serviceScope = app.ApplicationServices.CreateScope())
      {
        serviceScope.ServiceProvider.GetService<IDataBaseInitializer>().Initialize();
      }

      app.UseMvc();
    }
  }
}
