using Microsoft.EntityFrameworkCore;
using Spotifalso.Infrastructure.Data.Config;
using System;

namespace Spotifalso.IntegrationTests.Fixtures
{
    public  class BaseEfRepoTestFixture : IDisposable
    {
        public SpotifalsoDBContext Context => InMemoryContext();

        private static SpotifalsoDBContext InMemoryContext()
        {
            var options = new DbContextOptionsBuilder<SpotifalsoDBContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options;

            var context = new SpotifalsoDBContext(options);

            return context;
        }

        public void Dispose()
        {
            Context?.Dispose();
        }
    }
}
