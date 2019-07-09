using System.Threading.Tasks;

namespace Actio.Infraestructure.Config.Mongo
{
  public interface IDataBaseInitializer
  {
    Task Initialize();
  }
}