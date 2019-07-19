using System;
using Actio.Common.Criptography;
using Actio.Common.Exceptions;

namespace Actio.Services.Identity.Domain.Models
{
  public class User
  {
    public Guid Id { get; protected set; }
    public string Email { get; protected set; }
    public string Password { get; protected set; }
    public string Name { get; protected set; }
    public string Salt { get; protected set; }
    public DateTime CreatedAt { get; protected set; }

    public User() { }

    public User(string email, string name)
    {
      if (string.IsNullOrEmpty(email))
        throw new ActioException(code: "empty_user_email", message: $"User email can not be empty.");

      if (string.IsNullOrEmpty(name))
        throw new ActioException(code: "empty_user_name", message: $"User name can not be empty.");

      Id = Guid.NewGuid();
      Email = email.ToLowerInvariant();
      Name = name;
      CreatedAt = DateTime.UtcNow;
    }

    public void SetPassword(string password, IEncrypter encrypter)
    {
      if (string.IsNullOrEmpty(password))
        throw new ActioException(code: "empty_user_password", message: $"User password can not be empty.");

      Salt = encrypter.GetSalt();
      Password = encrypter.GetHash(password, Salt);
    }

    public bool ValidatePassword(string password, IEncrypter encrypter)
      => Password.Equals(encrypter.GetHash(password, Salt));
  }
}