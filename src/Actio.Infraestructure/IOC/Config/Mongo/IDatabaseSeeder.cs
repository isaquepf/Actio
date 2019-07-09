using System.Threading.Tasks;

namespace Actio.Infraestructure.IOC.Config.Mongo
{
  public interface IDatabaseSeeder
  {
    Task Seed();
  }
}