using Microsoft.Extensions.DependencyInjection;
using Spotifalso.Aplication.Interfaces.Services.Caching;
using Spotifalso.IntegrationTests.Fixtures;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Spotifalso.IntegrationTests.Caching
{
    public class AuthCacheServiceIntegrationTest : IClassFixture<BaseServicesTestFixture>
    {
        private readonly BaseServicesTestFixture _fixture;
        private readonly IAuthCacheService _authCacheService;

        public AuthCacheServiceIntegrationTest(BaseServicesTestFixture fixture)
        {
            _fixture = fixture;
            _authCacheService = _fixture.ServiceProvider.GetRequiredService<IAuthCacheService>();
        }

        [Fact]
        public async Task Should_SetTokenCacheAsync()
        {
            var id = Guid.NewGuid();
            var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImFkbWluIiwicm9sZSI6IkFkbWluIiwibmJmIjoxNjM3Mjg0MDc4LCJleHAiOjE2Mzc4ODg4NzgsImlhdCI6MTYzNzI4NDA3OH0.ytArKevbhZTD_tQ0p0JmHgMPTEQL8xqFe6tKuPT_sEg";

            await _authCacheService.SetTokenCacheAsync(id, token);
            var fromCache = await _authCacheService.GetTokenCacheAsync(id);

            Assert.NotNull(fromCache);
            Assert.Equal(token, fromCache);
        }

        [Fact]
        public async Task Should_SetTokenCacheAsync_TwoTimes_And_Return_Latest()
        {
            var id = Guid.NewGuid();
            var token1 = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImFkbWluIiwicm9sZSI6IkFkbWluIiwibmJmIjoxNjM3Mjg0MDc4LCJleHAiOjE2Mzc4ODg4NzgsImlhdCI6MTYzNzI4NDA3OH0.ytArKevbhZTD_tQ0p0JmHgMPTEQL8xqFe6tKuPT_sEg";
            var token2 = "g4NzgsImlhdCI6MTYzNzI4NDA3OH0.ytArKevbhZTD_tQ0p0JmHgMPTEQL8xqFe6tKuPT_sEg";

            await _authCacheService.SetTokenCacheAsync(id, token1);
            await _authCacheService.SetTokenCacheAsync(id, token2);

            var fromCache = await _authCacheService.GetTokenCacheAsync(id);

            Assert.NotNull(fromCache);
            Assert.Equal(token2, fromCache);
        }
    }
}
