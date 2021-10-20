using Amazon.KeyManagementService;
using Microsoft.Extensions.DependencyInjection;
using Spotifalso.Aplication.Interfaces.Infrastructure;
using Spotifalso.Infrastructure.AWS;
using System.Threading.Tasks;
using Xunit;

namespace Spotifalso.IntegrationTest.AWS
{
    public class KeyManagementServiceIntegrationTest
    {
        private readonly ServiceCollection _serviceCollection;
        private readonly ServiceProvider _serviceProvider;
        private readonly IKeyManagementService _keyManagementService;
        public KeyManagementServiceIntegrationTest()
        {
            //TODO Refactor to separete class
            _serviceCollection = new ServiceCollection();
            _serviceCollection.AddLogging();
            _serviceCollection.AddAWSService<IAmazonKeyManagementService>();
            _serviceCollection.AddScoped<IKeyManagementService, KeyManagementService>();
            _serviceProvider = _serviceCollection.BuildServiceProvider();

            _keyManagementService = _serviceProvider.GetRequiredService<IKeyManagementService>();
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
