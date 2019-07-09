using System;

namespace Actio.Domain.Core.Events
{
  public interface IAutenticatedEvent : IEvent
  {
    Guid UserId { get; }
  }
}