using RASA.Common.Entities.RabbitMQ;

namespace RASA.Utility.Helper;

using Microsoft.Extensions.Configuration;

public abstract class SandBoxHelper
{
    private static readonly string ConnectionJson = Path.Combine(Directory.GetCurrentDirectory(), "SandBox/connection.json");

    public static RabbitMqConnection RabbitMqConnection()
    {
        var builder = new ConfigurationBuilder().AddJsonFile(ConnectionJson);
        var connectionString = builder.Build().GetSection("RabbitMq");
        return new RabbitMqConnection(
            connectionString.GetSection("Host").Value ?? "",
            connectionString.GetSection("UserName").Value ?? "",
            connectionString.GetSection("Password").Value ?? "",
            int.Parse(connectionString.GetSection("Port").Value ?? ""));
    }
}