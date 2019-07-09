using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Actio.Domain.Model;

namespace Actio.Domain.Repositories
{
  public interface IActivityRepository
  {
    Task<Activity> GetActivity(Guid id);

    Task Add(Activity activity);

    Task<Activity> GetActivityByUser(Guid userId);

    Task<IEnumerable<Activity>> GetActivities(Guid userId);
  }
}
