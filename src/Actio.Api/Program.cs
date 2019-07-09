﻿using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Actio.Api
{
  public class Program
  {
    public static void Main(string[] args)
    {
      CreateWebHostBuilder(args).Build().Run();
    }

    public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
               .UseSetting("https_port", "8080")
               .UseStartup<Startup>();
  }
}
