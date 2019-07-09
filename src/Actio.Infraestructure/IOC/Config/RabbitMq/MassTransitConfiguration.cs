using System;
using System.Threading;
using System.Threading.Tasks;
using Actio.Application.Handlers;
using Actio.Application.Services;
using Actio.Application.Services.Identities;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Actio.Infraestructure.IOC.Config
{
  public static class MassTransitConfiguration
  {
    public static void AddRabbitMq(this IServiceCollection services)
    {
      services.AddMassTransit(_ => _.AddBus(ConfigureBus));
      services.AddSingleton(_ => ConfigureBus(_));
      services.AddSingleton<IHostedService, BusService>();
    }


    private static IBusControl ConfigureBus(IServiceProvider provider)
    {
      var options = provider.GetRequiredService<IOptions<RabbitConfig>>().Value;
      var bus = Bus.Factory.CreateUsingRabbitMq(config =>
      {
        var host = config.Host(options.Host, options.VirtualHost, h =>
              {
                h.Username(options.Username);
                h.Password(options.Password);
              });

        config.ConfigureEndpoints(provider);


        IActivityService activityService = null;
        ILogger<CreateActivityHandler> activitylogger = null;
        IUserService userService = null;
        ILogger<UserCreatedHandler> userlogger = null;

        using (var scope = provider.CreateScope())
        {
          activityService = scope.ServiceProvider.GetService<IActivityService>();
          activitylogger = scope.ServiceProvider.GetService<ILogger<CreateActivityHandler>>();
          userService = scope.ServiceProvider.GetService<IUserService>();
          userlogger = scope.ServiceProvider.GetService<ILogger<UserCreatedHandler>>();
        }

        config.ReceiveEndpoint(host, conf =>
       {
         conf.Consumer<CreateActivityHandler>(()
            => new CreateActivityHandler(activityService, activitylogger)
         );

         conf.Consumer<UserCreatedHandler>(()
           => new UserCreatedHandler(userService, userlogger)
         );


       });
      });

      bus.Start();

      return bus;
    }

  }

  public class BusService : IHostedService
  {
    private readonly IBusControl _busControl;

    public BusService(IBusControl busControl)
     => _busControl = busControl;

    public async Task StartAsync(CancellationToken cancellationToken)
        => await _busControl.StartAsync(cancellationToken);


    public async Task StopAsync(CancellationToken cancellationToken)
        => await _busControl.StopAsync(cancellationToken);

  }
}