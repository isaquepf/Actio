using System;

namespace Actio.Domain.Core.Events
{
  public class ActivityCreated : IAutenticatedEvent
  {
    public Guid Id { get; }
    public Guid UserId { get; }
    public string Category { get; }
    public string Name { get; }
    public string Description { get; }
    public DateTime CreatedAt { get; }

    protected ActivityCreated() { }

    public ActivityCreated(Guid id, Guid userId, string category,
    string name, string description)
    {
      Id = id;
      UserId = userId;
      Category = category;
      Name = name;
      Description = description;
      CreatedAt = DateTime.UtcNow;
    }
  }
}