using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RASA.Infrastructure.Repositories.RabbitMQ;

public class RabbitMqRepository
{
    private static IModel? _channel;
    private static EventingBasicConsumer _consumer;

    private RabbitMqRepository? _instance = null;

    public RabbitMqRepository Instance => _instance ??= new RabbitMqRepository();

    private RabbitMqRepository()
    {
        var factory = new ConnectionFactory { HostName = "localhost" };
        using var connection = factory.CreateConnection();
        _channel = connection.CreateModel();
        _channel.QueueDeclare(queue: "sms",
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);
        _consumer = new EventingBasicConsumer(_channel);
    }

    public static void Listen(EventHandler<BasicDeliverEventArgs> eventHandler)
    {
        _consumer.Received += eventHandler;
        _channel.BasicConsume(queue: "sms", autoAck: true, consumer: _consumer);
    }
}