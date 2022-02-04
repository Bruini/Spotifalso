using Amazon.KeyManagementService;
using Amazon.KeyManagementService.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Spotifalso.Aplication.Interfaces.Infrastructure;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Spotifalso.Infrastructure.AWS
{
    public class KeyManagementService : IKeyManagementService
    {
        private readonly IAmazonKeyManagementService _kmsClient;
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;
        private string GetUserPasswordArn() => _configuration.GetSection("KmsARN").Value;

        public KeyManagementService(
            IAmazonKeyManagementService kmsClient,
            ILogger<KeyManagementService> logger,
            IConfiguration configuration)
        {
            _kmsClient = kmsClient;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<string> EncriptUserPassword(string password)
        {
            try
            {
                byte[] passwordByteArray = Encoding.ASCII.GetBytes(password);
                var passwordMemorystream = new MemoryStream(passwordByteArray);

                EncryptRequest encryptRequest = new EncryptRequest()
                {
                    KeyId = GetUserPasswordArn(),
                    Plaintext = passwordMemorystream,
                    EncryptionAlgorithm = EncryptionAlgorithmSpec.SYMMETRIC_DEFAULT
                };

                var encryptResponse = await _kmsClient.EncryptAsync(encryptRequest);

                if (encryptResponse is not null)
                {
                    return Convert.ToBase64String(encryptResponse.CiphertextBlob.ToArray());
                }
                else
                {
                    throw new Exception(); //TODO Add custom exception
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error on EncriptyUserPassword method", ex);
                throw;
            }

        }

        public async Task<string> DecriptUserPassword(string password)
        {
            try
            {
                var passwordFromBase64 = Convert.FromBase64String(password);
                var passwordMemoryStream = new MemoryStream(passwordFromBase64);

                var decryptRequest = new DecryptRequest()
                {
                    CiphertextBlob = passwordMemoryStream,
                    KeyId = GetUserPasswordArn(),
                    EncryptionAlgorithm = EncryptionAlgorithmSpec.SYMMETRIC_DEFAULT
                };

                var decryptResponse = await _kmsClient.DecryptAsync(decryptRequest);
                if (decryptResponse is not null)
                {
                    return Encoding.UTF8.GetString(decryptResponse.Plaintext.ToArray());
                }
                else
                {
                    throw new Exception(); //TODO Add custom exception
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error on DecriptUserPassword method", ex);
                throw;
            }
        }
    }
}
