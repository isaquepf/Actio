using Xunit;
using Actio.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;
using Moq;

namespace Actio.Api.Tests.Unit
{
    public class HomeControllerTest
    {
        [Fact]
        public void Home_Controller_Get_Should_Return_String_Content()
        {
            var controller = new HomeController();

            var result = controller.Get();

            var contentResult = result as ContentResult;

            contentResult.Should().NotBeNull();
            contentResult.Content.Should().BeEquivalentTo("vraw");            
        }
    }
}