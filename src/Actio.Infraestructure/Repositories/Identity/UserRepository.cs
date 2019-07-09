using System;
using System.Threading.Tasks;
using Actio.Services.Identity.Domain.Models;
using Actio.Services.Identity.Domain.Repositories;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Actio.Infraestructure.Repositories.Identity
{
  public class UserRepository : IUserRepository
  {
    private readonly IMongoDatabase _database;

    public UserRepository(IMongoDatabase database)
           => _database = database;

    private IMongoCollection<User> _collection
        => _database.GetCollection<User>("Users");
    public async Task Add(User user)
    {
      await _collection.InsertOneAsync(user);
    }

    public async Task<User> Find(Guid id)
    {
      return await _collection
              .AsQueryable()
              .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<User> Find(string email)
    {
      return await _collection
              .AsQueryable()
              .FirstOrDefaultAsync(p => p.Email == email.ToLowerInvariant());
    }
  }
}