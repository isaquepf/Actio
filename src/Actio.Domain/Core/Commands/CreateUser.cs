namespace Actio.Domain.Core.Commands
{
  public class CreateUser : ICommand
  {
    public string Email { get; set; }

    public string Password { get; set; }

    public string Name { get; set; }

  }
}