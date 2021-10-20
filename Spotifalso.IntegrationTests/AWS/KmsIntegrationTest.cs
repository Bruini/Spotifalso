using Amazon.KeyManagementService;
using Microsoft.Extensions.DependencyInjection;
using Spotifalso.Aplication.Interfaces.Infrastructure;
using Spotifalso.Infrastructure.AWS;
using System.Threading.Tasks;
using Xunit;

namespace Spotifalso.IntegrationTest.AWS
{
    public class KmsIntegrationTest
    {
        private readonly ServiceCollection _serviceCollection;
        private readonly ServiceProvider _serviceProvider;
        private readonly IKms _kms;
        public KmsIntegrationTest()
        {
            //TODO Refactor to separete class
            _serviceCollection = new ServiceCollection();
            _serviceCollection.AddLogging();
            _serviceCollection.AddAWSService<IAmazonKeyManagementService>();
            _serviceCollection.AddScoped<IKms, Kms>();
            _serviceProvider = _serviceCollection.BuildServiceProvider();

            _kms = _serviceProvider.GetRequiredService<IKms>();
        }

        [Fact]
        public async Task Should_EncriptUserPassword()
        {
            string password = "abc001";

            var encriptedPassword = await _kms.EncriptUserPassword(password);

            Assert.NotNull(encriptedPassword);
            Assert.NotEqual(string.Empty, encriptedPassword);
        }

        [Fact]
        public async Task Should_DecriptUserPassword()
        {
            string password = "abc123@xx";

            var encriptedPassword = await _kms.EncriptUserPassword(password);
            var decriptedPassowrd = await _kms.DecriptUserPassword(encriptedPassword);

            Assert.NotNull(encriptedPassword);
            Assert.Equal(password, decriptedPassowrd);
        }
    }
}
