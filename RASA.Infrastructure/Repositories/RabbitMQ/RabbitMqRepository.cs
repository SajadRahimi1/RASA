using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RASA.Utility.Helper;

namespace RASA.Infrastructure.Repositories.RabbitMQ;

public class RabbitMqRepository
{
    private static IModel? _channel;
    private static EventingBasicConsumer _consumer;

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
        _channel.QueueDeclare(queue: "sms",
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);
        _consumer = new EventingBasicConsumer(_channel);
    }

    public void Listen(EventHandler<BasicDeliverEventArgs> eventHandler)
    {
        _consumer.Received += eventHandler;
        _channel.BasicConsume(queue: "sms", autoAck: true, consumer: _consumer);
    }
}