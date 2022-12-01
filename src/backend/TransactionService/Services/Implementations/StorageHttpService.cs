using TransactionService.Dtos;
using TransactionService.Models;
using TransactionService.Services.Interfaces;

namespace TransactionService.Services.Implementations;

public class StorageHttpService : HttpBaseService, IStorageService
{
    public StorageHttpService(IHttpClientFactory httpClientFactory, IConfiguration config) 
        : base(httpClientFactory, config, "StorageService")
    {
    }
    
    public async Task<StorageGetDto?> GetByIdAsync(Guid id)
    {
        var httpClient = CreateJsonHttpClient();
        
        var url = $"{BaseUrl}/{id}";
        var httpRequest = new HttpRequestMessage(HttpMethod.Get, url);
        
        var responseMessage = await httpClient.SendAsync(httpRequest);
        return await DeserializeResponseAsync<StorageGetDto>(responseMessage);
    }
}