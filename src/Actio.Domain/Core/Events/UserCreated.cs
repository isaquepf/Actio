namespace Actio.Domain.Core.Events
{
  public class UserCreated : IEvent
  {
    public string Email { get; set; }
    public string Name { get; set; }

    protected UserCreated() { }

    public UserCreated(string email, string name)
    {
      Email = email;
      Name = name;
    }
  }
}