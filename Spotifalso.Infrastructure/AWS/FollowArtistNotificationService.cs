using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Spotifalso.Aplication.Interfaces.Infrastructure;

namespace Spotifalso.Infrastructure.AWS
{
    public class FollowArtistNotificationService : IFollowArtistNotificationService
    {
        private readonly IAmazonSimpleNotificationService _snsClient;
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;

        public FollowArtistNotificationService(IAmazonSimpleNotificationService amazonSimpleNotificationService,
            ILogger<FollowArtistNotificationService> logger,
            IConfiguration configuration)
        {
            _snsClient = amazonSimpleNotificationService;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<bool> SubscribeArtist(Guid artistId, Guid userId, string emailAddress)
        {
            try
            {
                var existAttributesSubscription = await FindSubscriptionByEmail(emailAddress);

                var artists = new List<Guid>();
                var filterPolice = new Dictionary<string, IEnumerable<Guid>>();

                string existFilterPolicy = string.Empty;
                if (existAttributesSubscription is not null && existAttributesSubscription.Item2.TryGetValue("FilterPolicy", out existFilterPolicy))
                {
                    var artistIDs = System.Text.Json.JsonSerializer.Deserialize<JsonArtistID>(existFilterPolicy);

                    if (artistIDs is not null && artistIDs.ArtistIDs.Any())
                    {
                        artists.AddRange(artistIDs.ArtistIDs.Select(x => Guid.Parse(x)).ToList());
                    }

                    artists.Add(artistId);

                    await _snsClient.UnsubscribeAsync(existAttributesSubscription.Item1);
                }
                else
                {
                    artists.Add(artistId);
                }

                filterPolice = new Dictionary<string, IEnumerable<Guid>>
                {
                   { "ArtistIDs", artists}
                };

                var attributes = new Dictionary<string, string>
                {
                    { "FilterPolicy", System.Text.Json.JsonSerializer.Serialize(filterPolice) }
                };

                var subscribe = new SubscribeRequest
                {
                    Protocol = "email",
                    TopicArn = GetFollowArtistTopicArn(),
                    Endpoint = emailAddress,
                    Attributes = attributes
                };

                var response = await _snsClient.SubscribeAsync(subscribe);

                if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                    return true;

                return false;
            }
            catch (Exception e)
            {
                _logger.LogError("Error on SubscribeArtist method", e);
                throw;
            }


        }


        public async Task<Tuple<string, Dictionary<string, string>>> FindSubscriptionByEmail(string email, string nextToken = null)
        {
            var listSubscriptionRequest = new ListSubscriptionsByTopicRequest(GetFollowArtistTopicArn(), nextToken);

            var response = await _snsClient.ListSubscriptionsByTopicAsync(listSubscriptionRequest);

            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                var subscription = response.Subscriptions.FirstOrDefault(x => x.Endpoint == email);

                if (subscription is not null)
                {
                    var attributes = await _snsClient.GetSubscriptionAttributesAsync(subscription.SubscriptionArn);

                    if (attributes.HttpStatusCode == System.Net.HttpStatusCode.OK)
                    {
                        return Tuple.Create(subscription.SubscriptionArn, attributes.Attributes);
                    }
                    else return null;
                }

                if (response.NextToken is not null)
                {
                    return await FindSubscriptionByEmail(email, response.NextToken);
                }
                else return null;
                
            }
            return null;
        }

        private string GetFollowArtistTopicArn() => _configuration.GetSection("FollowArtistTopicARN").Value;

    }

    public class JsonArtistID
    {
        public List<string> ArtistIDs { get; set; }
    }
}
