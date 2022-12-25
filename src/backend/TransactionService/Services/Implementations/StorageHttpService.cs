using System.Diagnostics;
using System.Net.Http;
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

    public async Task CallBrokenEndpointAsync() 
    {
        var httpClient = CreateJsonHttpClient();

        var url = $"{BaseUrl}/brokenEndpoint";
        var httpRequest = new HttpRequestMessage(HttpMethod.Post, url);

        var responseMessage = await httpClient.SendAsync(httpRequest);
    }

    public async Task<Tuple<HttpResponseMessage, TimeSpan>> SpeedTestAsync() 
    {
        var httpClient = CreateJsonHttpClient();
        var watch = new Stopwatch();

        var url = $"{BaseUrl}/speedTest";
        var httpRequest = new HttpRequestMessage(HttpMethod.Get, url);

        watch.Start();
        var responseMessage = await httpClient.SendAsync(httpRequest);
        watch.Stop();

        TimeSpan timeElapsed = TimeSpan.FromMilliseconds(watch.ElapsedMilliseconds);
        
        return new Tuple<HttpResponseMessage, TimeSpan>(responseMessage, timeElapsed);
    }
}