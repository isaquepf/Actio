using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Actio.Application.Services.RQ;
using Actio.Common.Exceptions;
using Actio.Domain.Model;
using Actio.Domain.Repositories;
using Mapster;

namespace Actio.Application.Services
{
  public class ActivityService : IActivityService
  {
    private readonly IActivityRepository _activityRepository;
    private readonly ICategoryRepository _categoryRepository;

    public ActivityService(IActivityRepository activityRepository
      , ICategoryRepository categoryRepository)
    {
      this._activityRepository = activityRepository;
      this._categoryRepository = categoryRepository;
    }

    public async Task AddActivity(ActivityRQ request)
    {
      var category = await _categoryRepository.GetCategory(request.Category);

      if (category == null)
        throw new ActioException(code: "category_not_found", message: $"Category:'{request.Category}' was not found.");

      var activity = request.Adapt<Activity>()
                            .SetCategory(category);

      await _activityRepository.Add(activity);
    }

    public async Task<IEnumerable<ActivityRQ>> GetActivities(Guid userId)
    {
      var activities = await _activityRepository.GetActivities(userId);
      return activities.Adapt<IEnumerable<ActivityRQ>>();
    }

    public async Task<ActivityRQ> GetActivity(Guid id)
    {
      var activities = await _activityRepository.GetActivity(id);
      return activities.Adapt<ActivityRQ>();
    }
  }
}