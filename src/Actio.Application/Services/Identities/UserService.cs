using System.Threading.Tasks;
using Actio.Application.Services.Identities.RQ;
using Actio.Common.Criptography;
using Actio.Common.Exceptions;
using Actio.Services.Identity.Domain.Models;
using Actio.Services.Identity.Domain.Repositories;
using Mapster;


namespace Actio.Application.Services.Identities
{
  public class UserService : IUserService
  {
    private readonly IUserRepository _repository;
    private readonly IEncrypter _encrypter;
    private readonly IJWTHandler _jwtHandler;
    public UserService(IUserRepository repository, IEncrypter encrypter, IJWTHandler jwtHandler)
    {
      _repository = repository;
      _encrypter = encrypter;
      _jwtHandler = jwtHandler;
    }
    public async Task<JsonWebToken> Login(UserRQ userRQ)
    {
      var user = await _repository.Find(userRQ.Email);
      if (user == null)
        throw new ActioException("invalid_credentials"
            , $"Invalid Credentials");  
      
      if (!user.ValidatePassword(userRQ.Password, _encrypter))
        throw new ActioException("invalid_credentials"
              , $"Invalid Credentials");

      return _jwtHandler.Create(user.Id);
    }

    public async Task Register(UserRQ userRQ)
    {
      var user = await _repository.Find(userRQ.Email);
      if (user != null)
        throw new ActioException("email_in_use"
        , $"Email '{userRQ.Email}' is already in use.");

      user = userRQ.Adapt<User>();
      user.SetPassword(userRQ.Password, _encrypter);

      await _repository.Add(user);
    }
  }
}