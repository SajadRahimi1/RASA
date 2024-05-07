using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RASA.Common.Entities.Request;
using RASA.Infrastructure.Repositories.HttpClient;
using RASA.Utility.Helper;

namespace RASA.Infrastructure.Repositories.RabbitMQ;

public class RabbitMqRepository
{
    private static IModel? _channel;
    private static EventingBasicConsumer? _consumer;
    private readonly HttpClientRepository _httpClientRepository = new(new System.Net.Http.HttpClient());

    private static RabbitMqRepository? _instance = null;

    public static RabbitMqRepository Instance => _instance ??= new RabbitMqRepository();

    private RabbitMqRepository()
    {
        var rabbitMqConnection = SandBoxHelper.RabbitMqConnection();
        var factory = new ConnectionFactory
        {
            HostName = rabbitMqConnection.HostName, UserName = rabbitMqConnection.UserName,
            Password = rabbitMqConnection.Password, Port = rabbitMqConnection.Port
        };
        
        using var connection = factory.CreateConnection();
        _channel = connection.CreateModel();
        _channel.QueueDeclare(queue: "RASA",
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);
        _consumer = new EventingBasicConsumer(_channel);
        
        Listen();
    }

    private void Listen()
    {
        _consumer.Received += async (model,  ea)=>
        {
            var json = Encoding.UTF8.GetString(ea.Body.ToArray());
            Console.WriteLine(json);
            var request = JsonConvert.DeserializeObject<RequestEntity>(json);
            if (request is not null)
            {
                var response =await _httpClientRepository.SendAsync(request);
                var r = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Response {r}");
            }
        };
        _channel.BasicConsume(queue: "RASA", autoAck: true, consumer: _consumer);
    }
}