using KursAzureZad2.Models;
using System.Threading.Tasks;

namespace KursAzureZad2.Services
{
    public interface IAzureStorageAccountService
    {
        public Task<AppVersionModel> GetQueueAndReturnBlob();
    }
}
