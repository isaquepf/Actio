namespace Actio.Common.Criptography
{
  public interface IEncrypter
  {
    string GetSalt(string value);
    string GetHash(string value, string salt);
  }
}