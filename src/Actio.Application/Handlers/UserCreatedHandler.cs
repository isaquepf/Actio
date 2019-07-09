using System;
using System.Threading.Tasks;
using Actio.Application.Services.Identities;
using Actio.Application.Services.Identities.RQ;
using Actio.Common.Exceptions;
using Actio.Domain.Core.Commands;
using Actio.Domain.Core.Events;
using Mapster;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Actio.Application.Handlers
{
  public class UserCreatedHandler : ICommandHandler<CreateUser>, IConsumer<CreateUser>
  {
    private ConsumeContext<CreateUser> _context;
    private readonly ILogger _logger;
    private readonly IUserService _app;
    public UserCreatedHandler(IUserService app, ILogger<UserCreatedHandler> logger)
    {
      _app = app;
      _logger = logger;
    }

    public async Task Consume(ConsumeContext<CreateUser> context)
    {
      _context = context;
      await HandleAsync(_context.Message);
    }

    public async Task HandleAsync(CreateUser @command)
    {
      _logger.LogInformation($"Creating user: {@command.Email} {@command.Name}");

      try
      {
        await _app.Register(@command.Adapt<UserRQ>());

        await _context.Publish(new UserCreated(@command.Email, @command.Name));
        return;
      }
      catch (ActioException actioException)
      {
        await _context.Publish(new CreateUserRejected(@command.Email, actioException.Code, actioException.Message));
        _logger.LogError(actioException.Message);
      }
      catch (Exception exception)
      {
        await _context.Publish(new CreateUserRejected(@command.Email, "error", exception.Message));
        _logger.LogError(exception.Message);
      }
    }
  }
}