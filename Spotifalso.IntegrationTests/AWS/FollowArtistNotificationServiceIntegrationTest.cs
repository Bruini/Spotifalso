using Microsoft.Extensions.DependencyInjection;
using Spotifalso.Aplication.Interfaces.Infrastructure;
using Spotifalso.IntegrationTests.Fixtures;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Spotifalso.IntegrationTests.AWS
{
    public class FollowArtistNotificationServiceIntegrationTest : IClassFixture<BaseServicesTestFixture>
    {
        private readonly BaseServicesTestFixture _fixture;
        private readonly IArtistNotificationService _followArtistNotificationService;
        public FollowArtistNotificationServiceIntegrationTest(BaseServicesTestFixture fixture)
        {
            _fixture = fixture;
            _followArtistNotificationService = _fixture.ServiceProvider.GetRequiredService<IArtistNotificationService>();
        }

        [Fact]
        public async Task Should_SubscribeArtist()
        {
            var artistID = Guid.NewGuid();
            var userID = Guid.NewGuid();
            var email = "pedro.bruini@hotmail.com";

            var response = await _followArtistNotificationService.FollowArtist(artistID, userID, email);

            Assert.True(response);
        }
    }
}
