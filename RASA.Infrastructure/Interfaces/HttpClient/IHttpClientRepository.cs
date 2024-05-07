using RASA.Common.Entities.Request;

namespace RASA.Infrastructure.Interfaces.HttpClient;

public interface IHttpClientRepository
{
    Task<HttpResponseMessage> SendAsync(RequestEntity requestEntity);
}