using Actio.Application.Services;
using Actio.Application.Handlers;
using Actio.Domain.Core.Commands;
using Actio.Domain.Core.Events;
using Actio.Domain.Repositories;
using Actio.Infraestructure.IOC.Config.Mongo;
using Actio.Infraestructure.Repositories;
using Actio.Infraestructure.Seed;
using Microsoft.Extensions.DependencyInjection;
using Actio.Common.Criptography;
using Actio.Infraestructure.Repositories.Identity;
using Actio.Services.Identity.Domain.Repositories;
using Actio.Application.Services.Identities;

namespace Actio.Infraestructure.Config.IOC
{
  public static class ActioDependencyRegisterExtension
  {
    public static void AddActivities(this IServiceCollection services)
    {
      services.AddScoped<ICommandHandler<CreateActivity>, CreateActivityHandler>();
      services.AddScoped<IEventHandler<ActivityCreated>, ActivityCreatedHandler>();
      services.AddScoped<ICategoryRepository, CategoryRepository>();
      services.AddScoped<IActivityRepository, ActivityRepository>();
      services.AddScoped<IDatabaseSeeder, CustomMongoSeeder>();
      services.AddScoped<IActivityService, ActivityService>();
      services.AddScoped<IEncrypter, Encrypter>();

    }

    public static void AddUsers(this IServiceCollection services)
    {
      services.AddScoped<IUserRepository, UserRepository>();
      services.AddScoped<ICommandHandler<CreateUser>, UserCreatedHandler>();
      services.AddScoped<IUserService, UserService>();
      services.AddScoped<IEncrypter, Encrypter>();
    }
  }
}