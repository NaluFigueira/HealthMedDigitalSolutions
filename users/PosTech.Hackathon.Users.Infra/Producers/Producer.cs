using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using PosTech.Hackathon.Users.Infra.Interfaces;
using RabbitMQ.Client;

namespace PosTech.Hackathon.Users.Infra.Producers;

public class Producer(IConfiguration configuration) : IProducer
{
    private readonly IConfiguration _configuration = configuration;
    public void PublishMessageOnQueue<T>(T messageBody, string queueName)
    {
        var hostName = _configuration.GetConnectionString("RabbitMQConnectionHostName");
        var port = int.Parse(_configuration.GetConnectionString("RabbitMQConnectionPort")!);
        var userName = _configuration.GetConnectionString("RabbitMQConnectionUser");
        var password = _configuration.GetConnectionString("RabbitMQConnectionPassword");
        var factory = new ConnectionFactory() { HostName = hostName, Port = port, UserName = userName, Password = password };
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare(
            queue: queueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        string message = JsonSerializer.Serialize(messageBody);
        var body = Encoding.UTF8.GetBytes(message);

        channel.BasicPublish(
            exchange: "",
            routingKey: queueName,
            basicProperties: null,
            body);
    }
}
