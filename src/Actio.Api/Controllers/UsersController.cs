using System.Threading.Tasks;
using Actio.Domain.Core.Commands;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace Actio.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class UsersController : ControllerBase
  {
    private readonly IBusControl _bus;

    public UsersController(IBusControl bus)
      => _bus = bus;

    [HttpPost("")]
    public async Task<IActionResult> Post([FromBody]CreateUser command)
    {
      await _bus.Publish(command);
      return Accepted();
    }
  }
}