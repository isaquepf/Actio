using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Actio.Domain.Model;
using Actio.Domain.Repositories;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Actio.Infraestructure.Repositories
{
  public class ActivityRepository : IActivityRepository
  {
    private readonly IMongoDatabase _database;

    public ActivityRepository(IMongoDatabase database)
           => _database = database;

    private IMongoCollection<Activity> _collection
        => _database.GetCollection<Activity>("Activities");

    public async Task Add(Activity activity)
        => await _collection.InsertOneAsync(activity);

    public async Task<Activity> GetActivity(Guid id)
        => await _collection.AsQueryable().FirstOrDefaultAsync(p => p.Id == id);

    public async Task<IEnumerable<Activity>> GetActivities(Guid userId)
        => await _collection.AsQueryable().Where(p => p.UserId == userId).ToListAsync();

    public async Task<Activity> GetActivityByUser(Guid userId)
        => await _collection.AsQueryable().FirstOrDefaultAsync(p => p.UserId == userId);
  }
}