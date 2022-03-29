using Microsoft.Extensions.Logging;
using Nest;
using Spotifalso.Aplication.Interfaces.Infrastructure;
using Spotifalso.Core.Models;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Spotifalso.Infrastructure.Data.Search
{
    public class MusicSearchService : IMusicSearchService
    {
        private readonly IElasticClient _client;
        private readonly ILogger _logger;
        public MusicSearchService(IElasticClient client, ILogger<MusicSearchService> logger)
        {
            _client = client;
            _logger = logger;
        }

        public async Task<bool> Upload(Music music)
        {
            try
            {
                var indexResponse = await _client.IndexDocumentAsync(music);
                return indexResponse.IsValid;
            }
            catch (Exception e)
            {
                _logger.LogError($"Ocorreu um erro ao indexar o documento {JsonSerializer.Serialize(music)}", e);
                throw;
            }
           
        }
    }
}
