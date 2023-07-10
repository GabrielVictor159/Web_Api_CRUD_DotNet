   using System;
using RabbitMQ.Client;
using System.Text;
using System.Collections.Generic;
using gcsb.ecommerce.application.Interfaces.Messages;

namespace gcsb.ecommerce.infrastructure.Messages
{
public class MessageSender : IMessageSender
{
    private readonly IConnection _connection;
    private readonly IModel _channel;

     public MessageSender()
    {
        string hostName = Environment.GetEnvironmentVariable("RABBITMQ_HOST") ?? "localhost";
        string userName = Environment.GetEnvironmentVariable("RABBITMQ_USERNAME") ?? "guest";
        string password = Environment.GetEnvironmentVariable("RABBITMQ_PASSWORD") ?? "guest";
        int port = int.Parse(Environment.GetEnvironmentVariable("RABBITMQ_PORT") ?? "5672");

        var factory = new ConnectionFactory
        {
            HostName = hostName,
            UserName = userName,
            Port = port,
            Password = password
        };

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
    }

    public void SendDirectExchangeMessage(string exchangeName, string routingKey, string message)
    {
        _channel.ExchangeDeclare(exchange: exchangeName, type: ExchangeType.Direct, durable:true);

        var body = Encoding.UTF8.GetBytes(message);
        _channel.BasicPublish(exchange: exchangeName, routingKey: routingKey, basicProperties: null, body: body);

        Console.WriteLine("Direct Exchange Message Sent: {0}", message);
    }

    public void SendTopicExchangeMessage(string exchangeName, string routingKey, string message)
    {
        _channel.ExchangeDeclare(exchange: exchangeName, type: ExchangeType.Topic, durable:true);

        var body = Encoding.UTF8.GetBytes(message);
        _channel.BasicPublish(exchange: exchangeName, routingKey: routingKey, basicProperties: null, body: body);

        Console.WriteLine("Topic Exchange Message Sent: {0}", message);
    }

    public void SendFanoutExchangeMessage(string exchangeName, string message)
    {
        _channel.ExchangeDeclare(exchange: exchangeName, type: ExchangeType.Fanout, durable:true);

        var body = Encoding.UTF8.GetBytes(message);
        _channel.BasicPublish(exchange: exchangeName, routingKey: "", basicProperties: null, body: body);

        Console.WriteLine("Fanout Exchange Message Sent: {0}", message);
    }

    public void SendHeadersExchangeMessage(string exchangeName, string message, IDictionary<string, object> headers)
    {
        _channel.ExchangeDeclare(exchange: exchangeName, type: ExchangeType.Headers, durable:true);

        var body = Encoding.UTF8.GetBytes(message);

        var basicProperties = _channel.CreateBasicProperties();
        basicProperties.Headers = headers;

        _channel.BasicPublish(exchange: exchangeName, routingKey: "", basicProperties: basicProperties, body: body);

        Console.WriteLine("Headers Exchange Message Sent: {0}", message);
    }

    public void CloseConnection()
    {
        _channel.Close();
        _connection.Close();
    }

   
    }

}