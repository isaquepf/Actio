using System.Collections.Generic;
using System.Threading.Tasks;
using Actio.Domain.Model;
using Actio.Domain.Repositories;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Actio.Infraestructure.Repositories
{
  public class CategoryRepository : ICategoryRepository
  {

    private readonly IMongoDatabase _database;
    private IMongoCollection<Category> _collection
        => _database.GetCollection<Category>("Categories");

    public CategoryRepository(IMongoDatabase database)
        => _database = database;

    public async Task Add(Category category)
        => await _collection.InsertOneAsync(category);

    public async Task<Category> GetCategory(string name)
        => await _collection
                .AsQueryable()
                .FirstOrDefaultAsync(p => p.Name.Contains(name));

    public async Task<IEnumerable<Category>> ListCategories()
        => await _collection
                .AsQueryable()
                .ToListAsync();
  }
}