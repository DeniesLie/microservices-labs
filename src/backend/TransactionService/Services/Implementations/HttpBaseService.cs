using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;
using TransactionService.Exceptions;

namespace TransactionService.Services.Implementations;

public abstract class HttpBaseService
{
    protected readonly IHttpClientFactory HttpClientFactory;
    protected readonly string BaseUrl;
    protected readonly string ServiceName;
    
    public HttpBaseService(
        IHttpClientFactory httpClientFactory,
        IConfiguration config,
        string serviceName)
    {
        HttpClientFactory = httpClientFactory;
        BaseUrl = config[$"Services:{serviceName}:Url"];
        ServiceName = serviceName;
    }

    protected async Task<TResponse?> DeserializeResponseAsync<TResponse>(HttpResponseMessage response)
    {
        if (response.StatusCode == HttpStatusCode.NotFound)
            return default(TResponse);
        
        if (!response.IsSuccessStatusCode)
            throw new ServiceResponseException(ServiceName, response.StatusCode);

        using var contentStream = await response.Content.ReadAsStreamAsync();

        var ignoreCasingOptions = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
        TResponse? result = await JsonSerializer.DeserializeAsync<TResponse>(contentStream, ignoreCasingOptions);

        return result;
    }

    protected HttpClient CreateJsonHttpClient()
    {
        var httpClient = HttpClientFactory.CreateClient();
        
        var jsonAcceptHeader = new MediaTypeWithQualityHeaderValue("application/json");
        httpClient.DefaultRequestHeaders.Accept.Add(jsonAcceptHeader);

        return httpClient;
    }
}