using System;
using System.Threading.Tasks;
using Actio.Application.Services;
using Actio.Application.Services.RQ;
using Actio.Domain.Core.Events;
using Actio.Domain.Repositories;
using Mapster;

namespace Actio.Application.Handlers
{
  public class ActivityCreatedHandler : IEventHandler<ActivityCreated>
  {
    private readonly IActivityService _app;

    public ActivityCreatedHandler(IActivityService app)
    {
      _app = app;
    }

    public async Task HandleAsync(ActivityCreated @event)
    {      
      var rq = @event.Adapt<ActivityRQ>();
      await _app.AddActivity(rq);

      Console.WriteLine($"Activity created: {@event.Name}");
    }
  }
}