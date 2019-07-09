using System.Collections.Generic;
using System.Threading.Tasks;
using Actio.Infraestructure.Config.Mongo;
using Actio.Infraestructure.IOC.Config.Mongo;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace Actio.Infraestructure.IOC.Mongo
{
  public class MongoInitializer : IDataBaseInitializer
  {
    private readonly IOptions<MongoOptions> _options;
    private readonly IMongoDatabase _database;
    private readonly IDatabaseSeeder _seeder;

    public MongoInitializer(IMongoDatabase database,
        IDatabaseSeeder seeder,
        IOptions<MongoOptions> options)
    {
      _database = database;
      _seeder = seeder;
      _options = options;
    }

    private bool _initialized;
    private bool _seed => _options.Value.Seed;


    public async Task Initialize()
    {
      if (_initialized)
        return;

      RegisterConventions();

      _initialized = true;
      if (!_seed)
        return;

      await _seeder.Seed();
    }

    private void RegisterConventions()
         => ConventionRegistry.Register("ActioConventions", new MongoConvention(), x => true);

    private class MongoConvention : IConventionPack
    {
      public IEnumerable<IConvention> Conventions => new List<IConvention>
      {
          new IgnoreExtraElementsConvention(true),
          new EnumRepresentationConvention(BsonType.String),
          new CamelCaseElementNameConvention()
      };
    }
  }
}