using System;
using Actio.Infraestructure.Config.Mongo;
using Actio.Infraestructure.IOC.Config.Mongo;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Actio.Infraestructure.IOC.Mongo
{
  public static class MongoDependencyExtension
  {
    public static void AddMongoDB(this IServiceCollection services, IServiceProvider provider)
    {
      var options = provider.GetRequiredService<IOptions<MongoOptions>>().Value;

      services.AddSingleton<MongoClient>(m =>
      {
        return new MongoClient(options.ConnectionString);
      });

      services.AddScoped<IMongoDatabase>(m =>
      {
        var client = m.GetService<MongoClient>();
        return client.GetDatabase(options.DataBase);
      });

      services.AddScoped<IDataBaseInitializer, MongoInitializer>();
      services.AddScoped<IDatabaseSeeder, MongoSeeder>();

    }
  }
}