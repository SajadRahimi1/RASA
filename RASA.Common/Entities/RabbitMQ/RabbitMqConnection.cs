namespace RASA.Common.Entities.RabbitMQ;

public record RabbitMqConnection(string HostName,string UserName,string Password,int Port);