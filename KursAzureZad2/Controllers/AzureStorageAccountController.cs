using KursAzureZad2.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KursAzureZad2.Controllers
{
    [Route("api/azure-storage")]
    [ApiController]
    public class AzureStorageAccountController : ControllerBase
    {
        private readonly IAzureStorageAccountService _azureStorageAccountService;

        public AzureStorageAccountController(IAzureStorageAccountService azureStorageAccountService)
        {
            _azureStorageAccountService = azureStorageAccountService;
        }
        [HttpGet]
        public async Task<IActionResult> GetQueueAndReturnBlob()
        {
            var message = await _azureStorageAccountService.GetQueueAndReturnBlob();
            return Ok(message);
        }
    }
}
