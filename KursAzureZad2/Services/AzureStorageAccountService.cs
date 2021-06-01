using Azure.Storage.Blobs;
using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using KursAzureZad2.Models;
using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace KursAzureZad2.Services
{
    public class AzureStorageAccountService : IAzureStorageAccountService
    {
        public async Task<AppVersionModel> GetQueueAndReturnBlob()
        {
            var connectionString = "************";
            var queueName = "testq";
            var blobContainerName = "test-logs";

            var queueClient = new QueueClient(connectionString, queueName);
            var containerClient = new BlobContainerClient(connectionString, blobContainerName);


            QueueMessage[] message = await queueClient.ReceiveMessagesAsync();
            var messageContent = message[0].MessageText;

            byte[] data = Convert.FromBase64String(messageContent);
            var decodedMessage = Encoding.UTF8.GetString(data);

            var jsonMessage = JsonSerializer.Deserialize<AppVersionModel>(decodedMessage, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

            var blobClient = containerClient.GetBlobClient($"{Guid.NewGuid()}log.json");

            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(decodedMessage)))
            {
                await blobClient.UploadAsync(ms);
            }

                return jsonMessage;
        }
    }
}
