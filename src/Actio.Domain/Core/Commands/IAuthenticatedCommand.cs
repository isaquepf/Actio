using System;

namespace Actio.Domain.Core.Commands
{
  public interface IAuthenticatedCommand : ICommand
  {
    Guid UserId { get; set; }
  }
}