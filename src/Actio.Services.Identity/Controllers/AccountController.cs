using System;
using System.Threading.Tasks;
using Actio.Application.Services.Identities;
using Actio.Application.Services.Identities.RQ;
using Actio.Domain.Core.Commands;
using Microsoft.AspNetCore.Mvc;

namespace Actio.Services.Identity.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class AccountController : ControllerBase
  {
    private readonly IUserService _app;

    public AccountController(IUserService app)
        => this._app = app;

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] AuthenticateUser command)
    {
      try
      {
        var result = await _app.Login(
            new UserRQ { Email = command.Email, Password = command.Password }
        );
        return Ok(result);
      }
      catch (Exception exception)
      {
        return BadRequest(exception);
      }
    }
  }
}