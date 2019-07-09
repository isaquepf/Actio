using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Actio.Application.Services.RQ;

namespace Actio.Application.Services
{
  public interface IActivityService
  {
    Task AddActivity(ActivityRQ request);

    Task<IEnumerable<ActivityRQ>> GetActivities(Guid userId);

    Task<ActivityRQ> GetActivity(Guid id);
  }
}