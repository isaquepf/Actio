using System;
using System.Threading.Tasks;
using Actio.Services.Identity.Domain.Models;

namespace Actio.Services.Identity.Domain.Repositories
{
  public interface IUserRepository
  {
    Task<User> Find(Guid id);
    Task<User> Find(string email);
    Task Add(User user);
  }
}