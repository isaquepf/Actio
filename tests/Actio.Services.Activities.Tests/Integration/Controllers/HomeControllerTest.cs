using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Xunit;
using Actio.Services.Activities;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace Actio.Services.Activities.Tests.Integration.Controllers
{
    public class HomeControllerTest
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public HomeControllerTest()
        {        
            var startupAssembly = typeof(Startup).GetTypeInfo().Assembly;
            var contentRoot = Directory.GetCurrentDirectory();

            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var webHostBuilder = new WebHostBuilder()
                .UseContentRoot(contentRoot)                
                .UseConfiguration(configurationBuilder.Build())
                .UseEnvironment("Development")
                .UseStartup(typeof(Startup));
     
                
            _server = new TestServer(webHostBuilder);
            _client = _server.CreateClient();
        }

        [Fact]
        public async Task Home_Controller_Get_Should_Return_String_Content()
        {
            var response = await _client.GetAsync("api/values/5");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsByteArrayAsync();

            content.Should().BeEquivalentTo("value");

        }

    }
}