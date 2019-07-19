using Xunit;
using Moq;
using FluentAssertions;
using System.Threading.Tasks;
using Actio.Services.Identity.Domain.Repositories;
using Actio.Common.Criptography;
using System;
using Actio.Services.Identity.Domain.Models;
using Actio.Application.Services.Identities;
using Actio.Application.Services.Identities.RQ;

namespace Actio.Services.Identity.Tests.Unit.Services
{
    
    public class UserServiceTest
    {
        [Fact]
        public async Task User_Service_Login_Should_Return_JWT()
        {
            var salt = "salt";
            var password = "xyz";
            var hash = "hash";
            var token = "token";
            var email = "isaquepf@gmail.com";
            var name = "Isaque";

            var userRepositoryMock = new Mock<IUserRepository>();
            var encriptMock = new Mock<IEncrypter>();
            var jwtHandlerMock = new Mock<IJWTHandler>();

            encriptMock.Setup(x => x.GetSalt()).Returns(salt);
            encriptMock.Setup(x => x.GetHash(password, salt)).Returns(hash); 
            jwtHandlerMock.Setup(x => x.Create(It.IsAny<Guid>()))
                    .Returns(new JsonWebToken {
                        Token = token
                    });
            
            var user = new User(email, name);
            user.SetPassword(password, encriptMock.Object);
            userRepositoryMock.Setup(x => x.Find(email)).ReturnsAsync(user);

            var userService = new UserService(
                    userRepositoryMock.Object
                  , encriptMock.Object
                  , jwtHandlerMock.Object
            );

            var jwt = await userService.Login(new UserRQ {
                Email = email,
                Password = password
            });

            userRepositoryMock.Verify(p => p.Find(email), Times.Once);
            jwtHandlerMock.Verify(p => p.Create(It.IsAny<Guid>()), Times.Once);
            jwt.Should().NotBeNull();
            jwt.Token.Should().BeEquivalentTo(token);

        }
    }
}