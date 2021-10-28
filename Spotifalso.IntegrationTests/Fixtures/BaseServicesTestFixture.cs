using Amazon.KeyManagementService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Spotifalso.Aplication.Interfaces.Infrastructure;
using Spotifalso.Infrastructure.AWS;
using Spotifalso.Infrastructure.JWT;
using System.IO;

namespace Spotifalso.IntegrationTests.Fixtures
{
    public class BaseServicesTestFixture
    {
        public ServiceProvider ServiceProvider { get; private set; }
        public BaseServicesTestFixture()
        {
            var serviceCollection = new ServiceCollection();
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(@"appsettings.json", true, false)
                .Build();                

            serviceCollection.AddSingleton<IConfiguration>(configuration);
            serviceCollection.AddLogging();
            serviceCollection.AddAWSService<IAmazonKeyManagementService>();
            serviceCollection.AddScoped<IKeyManagementService, KeyManagementService>();
            serviceCollection.AddScoped<ITokenService, TokenService>();

            ServiceProvider = serviceCollection.BuildServiceProvider();
        }
    }
}
