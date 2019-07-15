using System;
using System.Threading.Tasks;
using Actio.Application.Services;
using Actio.Domain.Core.Commands;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Actio.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  [Authorize]
  public class ActivitiesController : ControllerBase
  {
    private readonly IBusControl _messageBus;
    private IActivityService _app;

    public ActivitiesController(IBusControl messageBus, IActivityService app)
    {
      this._messageBus = messageBus;
      _app = app;
    }

    [HttpPost("")]
    public async Task<IActionResult> Post([FromBody]CreateActivity command)
    {
      command.Id = Guid.NewGuid();
      command.CreatedAt = DateTime.Now;
      command.UserId = Guid.Parse(User.Identity.Name);
      await _messageBus.Publish<CreateActivity>(command);
      return Accepted($"activities/{command.Id}/");
    }

    [HttpGet("")]
    public async Task<IActionResult> Get()
    {
      var userId = Guid.Parse(User.Identity.Name);
      var activities = await _app.GetActivities(userId);
      return Ok(activities);
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
      var userId = Guid.Parse(User.Identity.Name);
      var activity = await _app.GetActivity(id);

      if (activity == null)
        return NotFound();

      if (activity.UserId != userId)
        return Unauthorized();

      return Ok(activity);
    }
  }
}