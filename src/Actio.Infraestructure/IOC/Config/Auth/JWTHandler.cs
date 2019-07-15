using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Actio.Services.Identity.Domain.Models;
using Actio.Services.Identity.Domain.Repositories;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Actio.Infraestructure.Config.Auth
{
  public class JWTHandler : IJWTHandler
  {
    private readonly JwtSecurityTokenHandler _jwtSecurityHandler = new JwtSecurityTokenHandler();
    private readonly JwtOptions _options;
    private readonly SecurityKey _issuerSigningKey;
    private readonly SigningCredentials _signingCredentials;
    private readonly JwtHeader _jwtHeader;
    private readonly TokenValidationParameters _tokenValidationParameters;
    public JWTHandler(IOptions<JwtOptions> options)
    {
      _options = options.Value;
      _issuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));
      _signingCredentials = new SigningCredentials(_issuerSigningKey, SecurityAlgorithms.HmacSha256);
      _jwtHeader = new JwtHeader(_signingCredentials);
      _tokenValidationParameters = new TokenValidationParameters
      {
        ValidateAudience = false,
        ValidIssuer = _options.Issuer,
        IssuerSigningKey = _issuerSigningKey
      };
    }

    public JsonWebToken Create(Guid userId)
    {
      var tokenHandler = new JwtSecurityTokenHandler();
      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(new Claim[]
          {
                    new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                    new Claim(JwtRegisteredClaimNames.UniqueName, userId.ToString())
          }),
        Expires = DateTime.UtcNow.AddDays(7),
        Issuer = _options.Issuer,
        SigningCredentials = _signingCredentials
      };
      var token = tokenHandler.CreateToken(tokenDescriptor);
      var tokenString = tokenHandler.WriteToken(token);

      return new JsonWebToken
      {
        Token = tokenString,
        Issuer = _options.Issuer,
        Expires = DateTime.UtcNow.AddDays(7).Ticks
      };
    }
  }
}