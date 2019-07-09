using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Actio.Infraestructure.IOC.Config.Mongo
{
  public class MongoSeeder : IDatabaseSeeder
  {
    protected IMongoDatabase Database;

    public MongoSeeder(IMongoDatabase database)
        => Database = database;

    public async Task Seed()
    {
      var collectionCursor = await Database.ListCollectionsAsync();
      var collections = await collectionCursor.ToListAsync();

      if (collections.Any())
        return;

      await CustomSeedAsync();
    }

    protected virtual async Task CustomSeedAsync()
    {
      await Task.CompletedTask;
    }
  }
}