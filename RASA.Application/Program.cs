using RASA.Infrastructure.Repositories.RabbitMQ;

var rabbitmq= RabbitMqRepository.Instance;
Console.WriteLine("Listen:.........");
Console.ReadKey();