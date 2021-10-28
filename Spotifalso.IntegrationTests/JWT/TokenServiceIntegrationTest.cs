using Microsoft.Extensions.DependencyInjection;
using Spotifalso.Aplication.Interfaces.Infrastructure;
using Spotifalso.Core.Enums;
using Spotifalso.Core.Models;
using Spotifalso.IntegrationTests.Fixtures;
using Xunit;

namespace Spotifalso.IntegrationTests.JWT
{
    public class TokenServiceIntegrationTest : IClassFixture<BaseServicesTestFixture>
    {
        private readonly BaseServicesTestFixture _fixture;
        private readonly ITokenService _tokenService;
        public TokenServiceIntegrationTest(BaseServicesTestFixture fixture)
        {
            _fixture = fixture;
            _tokenService = _fixture.ServiceProvider.GetRequiredService<ITokenService>();
        }

        [Theory]
        [InlineData(Roles.Admin)]
        [InlineData(Roles.Subscriber)]
        public void Should_GenerateToken(Roles role)
        {
            var user = new User(string.Empty, "abc001", role, "Pedro", string.Empty);
            var token = this._tokenService.GenerateToken(user);

            Assert.NotNull(token);
            Assert.NotEqual(string.Empty, token);
        }
    }
}
