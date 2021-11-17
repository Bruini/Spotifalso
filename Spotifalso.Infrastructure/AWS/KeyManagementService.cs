using Amazon.KeyManagementService;
using Amazon.KeyManagementService.Model;
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

        private const string USER_PASSWORD_ARN = "arn:aws:kms:us-east-1:817086899180:key/4072e4d2-117d-4bc8-b489-b7821fe50ddf";
        public KeyManagementService(IAmazonKeyManagementService kmsClient, ILogger<KeyManagementService> logger)
        {
            _kmsClient = kmsClient;
            _logger = logger;
        }

        public async Task<string> EncriptUserPassword(string password)
        {
            try
            {
                byte[] passwordByteArray = Encoding.ASCII.GetBytes(password);
                var passwordMemorystream = new MemoryStream(passwordByteArray);

                EncryptRequest encryptRequest = new EncryptRequest()
                {
                    KeyId = USER_PASSWORD_ARN,
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
                    KeyId = USER_PASSWORD_ARN,
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
