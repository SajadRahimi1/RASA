using RASA.Common.Entities.RabbitMQ;

namespace RASA.Utility.Helper;

using Microsoft.Extensions.Configuration;

public class SandBoxHelper
{
    private static readonly string ConnectionJson = Path.Combine(Directory.GetCurrentDirectory(), "SandBox/connection.json");

    public static RabbitMQConnection RabbitMqConnection()
    {
        var builder = new ConfigurationBuilder().AddJsonFile(ConnectionJson);
        var connectionString = builder.Build().GetSection("ConnectionStrings");
        return new RabbitMQConnection(
            connectionString.GetSection("Host").Value ?? "",
            connectionString.GetSection("UserName").Value ?? "",
            connectionString.GetSection("Password").Value ?? "",
            int.Parse(connectionString.GetSection("Port").Value ?? ""));
    }
}