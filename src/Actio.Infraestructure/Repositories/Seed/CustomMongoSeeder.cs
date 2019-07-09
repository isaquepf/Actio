using System.Linq;
using System.Threading.Tasks;
using Actio.Domain.Model;
using Actio.Domain.Repositories;
using Actio.Infraestructure.IOC.Config.Mongo;
using MongoDB.Driver;

namespace Actio.Infraestructure.Seed
{
  public class CustomMongoSeeder : MongoSeeder
  {
    private readonly ICategoryRepository _categoryRepository;

    public CustomMongoSeeder(IMongoDatabase database
    , ICategoryRepository categoryRepository)
        : base(database)
    {
      this._categoryRepository = categoryRepository;
    }

    protected override async Task CustomSeedAsync()
    {
      var categories = new string[] {
           "Work", "Sport", "Hobby"
      };

      await Task.WhenAll(categories.Select(
          c => _categoryRepository.Add(new Category(c))
      ));
    }
  }
}