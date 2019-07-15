using System;
using System.Text;
using Actio.Services.Identity.Domain.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;

namespace Actio.Infraestructure.Config.Auth
{
  public static class Extension
  {
    public static void AddJwt(this IServiceCollection services, IServiceProvider provider)
    {
      var options = provider.GetRequiredService<IOptions<JwtOptions>>().Value;

      services.AddSingleton<IJWTHandler, JWTHandler>();

      services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
              .AddJwtBearer(config =>
        {
          config.RequireHttpsMetadata = false;
          config.SaveToken = true;
          config.TokenValidationParameters = new TokenValidationParameters()
          {
            ValidateAudience = false,
            ValidIssuer = options.Issuer,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.SecretKey)),
          };
        });

      services.AddAuthorization();
    }
  }
}