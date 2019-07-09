using System.Threading.Tasks;

namespace Actio.Domain.Core.Commands
{
  public interface ICommandHandler<in T> where T : ICommand
  {
    Task HandleAsync(T @command);
  }
}