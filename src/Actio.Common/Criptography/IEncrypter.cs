namespace Actio.Common.Criptography
{
  public interface IEncrypter
  {
    string GetSalt();
    string GetHash(string value, string salt);
  }
}