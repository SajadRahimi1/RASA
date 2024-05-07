using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client.Events;
using RASA.Common.Entities.Request;
using RASA.Infrastructure.Repositories.HttpClient;
using RASA.Infrastructure.Repositories.RabbitMQ;

HttpClientRepository httpClientRepository = new(new HttpClient());

async void Handler(object? model, BasicDeliverEventArgs ea)
{
    var json = Encoding.UTF8.GetString(ea.Body.ToArray());
    var request = JsonConvert.DeserializeObject<RequestEntity>(json);

    if (request is not null) await httpClientRepository.SendAsync(request);
}

RabbitMqRepository.Instance.Listen(Handler);
Console.ReadKey();