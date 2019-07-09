using System.Collections.Generic;
using System.Threading.Tasks;
using Actio.Domain.Model;

namespace Actio.Domain.Repositories
{
  public interface ICategoryRepository
  {
    Task<Category> GetCategory(string name);

    Task<IEnumerable<Category>> ListCategories();

    Task Add(Category category);
  }
}