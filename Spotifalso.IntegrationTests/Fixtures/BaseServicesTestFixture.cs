using Amazon.KeyManagementService;
using Amazon.SimpleNotificationService;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Spotifalso.Aplication.Interfaces.Infrastructure;
using Spotifalso.Aplication.Interfaces.Services.Caching;
using Spotifalso.Aplication.Services.Caching;
using Spotifalso.Infrastructure.AWS;
using Spotifalso.Infrastructure.Cache;
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
            serviceCollection.AddAWSService<IAmazonSimpleNotificationService>();
            serviceCollection.AddScoped<IKeyManagementService, KeyManagementService>();
            serviceCollection.AddScoped<IFollowArtistNotificationService, FollowArtistNotificationService>();
            serviceCollection.AddScoped<ITokenService, TokenService>();
            serviceCollection.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetConnectionString("Redis");
            });
            serviceCollection.AddSingleton<IDistributedCache, RedisCache>();
            serviceCollection.AddSingleton<ICacheProvider, CacheProvider>();
            serviceCollection.AddSingleton<IAuthCacheService, AuthCacheService>();

            ServiceProvider = serviceCollection.BuildServiceProvider();
        }
    }
}
