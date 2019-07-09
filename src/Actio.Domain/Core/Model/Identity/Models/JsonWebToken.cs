namespace Actio.Services.Identity.Domain.Models
{
  public class JsonWebToken
  {
    public string Token { get; set; }

    public long Expires { get; set; }

    public string Issuer { get; set; }
  }
}