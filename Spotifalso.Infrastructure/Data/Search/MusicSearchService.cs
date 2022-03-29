using Microsoft.Extensions.Logging;
using Nest;
using Spotifalso.Aplication.Interfaces.Infrastructure;
using Spotifalso.Aplication.ViewModels;
using Spotifalso.Core.Models;
using Spotifalso.Infrastructure.Data.Search.Index;
using System;
using System.Collections.Generic;
using System.Linq;
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
            VerifyAndCreateIndices();
        }

        public async Task<bool> IndexAsync(Music music)
        {
            try
            {
                var musicIndex = new MusicIndex(music);
                var indexResponse = await _client.IndexAsync(musicIndex, descriptor => descriptor.Index(nameof(Music).ToLower()));
                return indexResponse.IsValid;
            }
            catch (Exception e)
            {
                _logger.LogError($"Ocorreu um erro ao indexar o documento {JsonSerializer.Serialize(music)}", e);
                throw;
            }
           
        }

        public async Task<IEnumerable<MusicViewModel>> SearchInAllFields(string term)
        {
            var fields = typeof(MusicIndex).GetProperties().Select(p => p.Name.ToLower()).ToArray();
            fields = fields.Where(x => x == "title").ToArray();

            var query = new QueryContainerDescriptor<MusicIndex>()
                .MultiMatch(c => c
                .Type(TextQueryType.PhrasePrefix)
                .Fields(f => f.Fields(fields))
                .Lenient()
                .Query(term));

            var response = await _client.SearchAsync<MusicIndex>(s => s.Query( q => query).Index(nameof(Music).ToLower()));

            var listDocuments = response.Documents.ToList();

            var result = new List<MusicViewModel>();

            foreach (var document in listDocuments)
            {
                result.Add(document.ConvertToViewModel());
            }

            return result;
        }

        private void VerifyAndCreateIndices()
        {
            if (!_client.Indices.Exists(nameof(Music).ToLower()).Exists)
                _client.Indices.Create(nameof(Music).ToLower());
        }
    }
}
