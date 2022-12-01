using System.Net;

namespace TransactionService.Exceptions;

public class ServiceResponseException : Exception
{
    public ServiceResponseException(string serviceName, HttpStatusCode statusCode)
        : base($"Service '{serviceName}' responded with error code {statusCode}")
    {
    }
}