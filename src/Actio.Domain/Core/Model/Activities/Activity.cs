using System;
using Actio.Common.Exceptions;

namespace Actio.Domain.Model
{
  public class Activity
  {
    public Guid Id { get; protected set; }
    public string Name { get; protected set; }
    public Category Category { get; protected set; }
    public string Description { get; set; }
    public Guid UserId { get; protected set; }
    public DateTime CreatedAt { get; protected set; }

    public Activity() { }

    public Activity(Guid id, string name, Category category, string description, Guid userId, DateTime createdAt)
    {
      if (string.IsNullOrEmpty(name))
        throw new ActioException(code: "empty_activity_name", message: $"Activity name can not be empty.");

      Id = Guid.NewGuid();
      Name = name.ToLowerInvariant();
      Category = category;
      Description = description;
      UserId = userId;
      CreatedAt = CreatedAt;
    }

    public Activity SetCategory(Category category)
    {
      Category = category;
      return this;
    }

  }
}