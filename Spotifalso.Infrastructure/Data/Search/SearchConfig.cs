using Nest;
using System;

namespace Spotifalso.Infrastructure.Data.Search
{
    public static class SearchConfig
    {
        private static readonly ConnectionSettings _connectionSettings;
        public static ElasticClient GetClient() => new ElasticClient(_connectionSettings);

		static SearchConfig()
		{
			_connectionSettings = new ConnectionSettings(new Uri($"http://localhost:9200"))
				.EnableDebugMode()
				.PrettyJson(true);
		}
	}
}
