using Microsoft.Extensions.DependencyInjection;
using Spotifalso.Aplication.Interfaces.Infrastructure;
using Spotifalso.IntegrationTests.Fixtures;
using System.Threading.Tasks;
using Xunit;

namespace Spotifalso.IntegrationTest.AWS
{
    public class KeyManagementServiceIntegrationTest: IClassFixture<BaseServicesTestFixture>
    {
        private readonly BaseServicesTestFixture _fixture;
        private readonly IKeyManagementService _keyManagementService;
        public KeyManagementServiceIntegrationTest(BaseServicesTestFixture fixture)
        {
            _fixture = fixture;
            _keyManagementService = _fixture.ServiceProvider.GetRequiredService<IKeyManagementService>();
        }

        [Fact]
        public async Task Should_EncriptUserPassword()
        {
            string password = "abc001";

            var encriptedPassword = await _keyManagementService.EncriptUserPassword(password);

            Assert.NotNull(encriptedPassword);
            Assert.NotEqual(string.Empty, encriptedPassword);
        }

        [Fact]
        public async Task Should_DecriptUserPassword()
        {
            string password = "abc123@xx";

            var encriptedPassword = await _keyManagementService.EncriptUserPassword(password);
            var decriptedPassowrd = await _keyManagementService.DecriptUserPassword(encriptedPassword);

            Assert.NotNull(encriptedPassword);
            Assert.Equal(password, decriptedPassowrd);
        }
    }
}
