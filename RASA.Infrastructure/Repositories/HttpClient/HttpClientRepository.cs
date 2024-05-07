using RASA.Common.Entities.Request;
using RASA.Infrastructure.Interfaces.HttpClient;



namespace RASA.Infrastructure.Repositories.HttpClient;

public class HttpClientRepository:IHttpClientRepository
{
   private readonly System.Net.Http.HttpClient _httpClient;

   public HttpClientRepository(System.Net.Http.HttpClient httpClient)
   {
       _httpClient = httpClient;
   }

   public async Task<HttpResponseMessage> SendAsync(RequestEntity requestEntity)
   {
       
       var requestMessage = new HttpRequestMessage()
       {
           Method = new HttpMethod(nameof(requestEntity.Method)),
           RequestUri = new Uri(requestEntity.Url)
       };

       foreach (var header in requestEntity.Header)   
       {
           requestMessage.Headers.Add(header.Key,header.Value);
       }

       if (requestEntity.Body.Any())
       {
           requestMessage.Content = new FormUrlEncodedContent(requestEntity.Body);
       }

       return await _httpClient.SendAsync(requestMessage);
   }
}