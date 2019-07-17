using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Actio.Api.Controllers;
using Actio.Application.Services;
using Actio.Domain.Core.Commands;
using Actio.Domain.Repositories;
using FluentAssertions;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Actio.Api.Tests.Unit
{
    public class ActivitiesControllerTest
    {
        [Fact]
        public async Task Activities_Controller_Post_Should_Return_Accepted()
        {
            var busMock = new Mock<IBusControl>();
            var appMock = new Mock<IActivityService>();
            var controller = new ActivitiesController(busMock.Object
                , appMock.Object);

            var userId = Guid.NewGuid();

            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext()
                {
                    User = new ClaimsPrincipal(
                        new ClaimsIdentity(new Claim[] {
                            new Claim(ClaimTypes.Name, userId.ToString())
                        }, "test")
                    )
                }
            };

            var command = new CreateActivity
            {
                Id = Guid.NewGuid(),
                UserId = userId
            };

            var result =  await controller.Post(command);
            var contentResult = result as AcceptedResult;
            contentResult.Should().NotBeNull();
            contentResult.Location.Should().BeEquivalentTo($"activities/{command.Id}/");
        }
    }
}