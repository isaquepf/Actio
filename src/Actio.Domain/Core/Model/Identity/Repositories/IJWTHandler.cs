using System;
using Actio.Services.Identity.Domain.Models;

namespace Actio.Services.Identity.Domain.Repositories
{
  public interface IJWTHandler
  {
    JsonWebToken Create(Guid userid);
  }
}