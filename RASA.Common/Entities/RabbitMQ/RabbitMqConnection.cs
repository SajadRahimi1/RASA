namespace RASA.Common.Entities.RabbitMQ;

public record RabbitMQConnection(string HostName,string UserName,string Password,int Port);