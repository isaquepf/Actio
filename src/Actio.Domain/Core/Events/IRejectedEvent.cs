namespace Actio.Domain.Core.Events
{
  public interface IRejectedEvent : IEvent
  {
    string Reason { get; }

    string Code { get; }
  }
}