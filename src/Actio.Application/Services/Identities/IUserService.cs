using System.Threading.Tasks;
using Actio.Application.Services.Identities.RQ;
using Actio.Services.Identity.Domain.Models;

namespace Actio.Application.Services.Identities
{
  public interface IUserService
  {
    Task Register(UserRQ user);

    Task<JsonWebToken> Login(UserRQ user);
  }
}