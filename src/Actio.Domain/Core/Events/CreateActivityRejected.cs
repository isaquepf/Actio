using System;

namespace Actio.Domain.Core.Events
{
  public class CreateActivityRejected : IRejectedEvent
  {
    public CreateActivityRejected(Guid id, string code, string reason)
    {
      Id = id;
      Code = code;
      Reason = reason;
    }
    public Guid Id { get; set; }
    public string Code { get; set; }
    public string Reason { get; set; }
  }
}