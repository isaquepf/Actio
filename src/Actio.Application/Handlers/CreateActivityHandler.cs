using System;
using System.Threading.Tasks;
using Mapster;
using Actio.Common.Exceptions;
using Actio.Domain.Core.Commands;
using Actio.Application.Services;
using MassTransit;
using Microsoft.Extensions.Logging;
using Actio.Domain.Core.Events;
using Actio.Application.Services.RQ;

namespace Actio.Application.Handlers
{
  public class CreateActivityHandler : ICommandHandler<CreateActivity>, IConsumer<CreateActivity>
  {
    private readonly IActivityService _app;
    private ConsumeContext<CreateActivity> _context;
    private readonly ILogger _logger;
    public CreateActivityHandler(IActivityService app, ILogger<CreateActivityHandler> logger)
    {
      _app = app;
      _logger = logger;
    }
    public async Task Consume(ConsumeContext<CreateActivity> context)
    {
      _context = context;
      await HandleAsync(context.Message);
    }

    public async Task HandleAsync(CreateActivity @command)
    {
      _logger.LogInformation($"Creating activity: {@command.Category} {@command.Name}");

      var activity = @command.Adapt<ActivityRQ>();

      try
      {
        await _app.AddActivity(activity);
        await _context.Publish(
            new ActivityCreated(@activity.Id
                              , @activity.UserId
                              , @activity.Category
                              , @activity.Name
                              , @activity.Description)
        );
        return;
      }
      catch (ActioException actioException)
      {
        await _context.Publish(new CreateActivityRejected(activity.Id, actioException.Code, actioException.Message));
        _logger.LogError(actioException.Message);
      }
      catch (Exception exception)
      {
        await _context.Publish(new CreateActivityRejected(activity.Id, "error", exception.Message));
        _logger.LogError(exception.Message);
      }
    }
  }
}