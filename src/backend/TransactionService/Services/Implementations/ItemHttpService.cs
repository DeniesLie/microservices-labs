using TransactionService.Dtos;
using TransactionService.Models;
using TransactionService.Services.Interfaces;

namespace TransactionService.Services.Implementations;

public class ItemHttpService : HttpBaseService, IItemService
{
    public ItemHttpService(IHttpClientFactory httpClientFactory, IConfiguration config) 
        : base(httpClientFactory, config, "ItemService")
    {
    }
    
    public async Task<ItemGetDto?> GetByIdAsync(Guid id)
    {
        var httpClient = CreateJsonHttpClient();

        var url = $"{BaseUrl}/{id}";
        var httpRequest = new HttpRequestMessage(HttpMethod.Get, url);
        
        var responseMessage = await httpClient.SendAsync(httpRequest);
        return await DeserializeResponseAsync<ItemGetDto>(responseMessage);
    }
}