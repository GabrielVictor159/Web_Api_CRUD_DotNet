using System;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Collections.Generic;
using gcsb.ecommerce.application.Interfaces.Messages;

namespace gcsb.ecommerce.infrastructure.Messages
{
    public class MessageReceiver : IMessageReceiver
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public MessageReceiver()
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

        public void ReceiveDirectExchangeMessages(string exchangeName, string queueName, string routingKey, Func<string, bool> messageHandler)
        {
            _channel.ExchangeDeclare(exchange: exchangeName, type: ExchangeType.Direct, durable:true);
            _channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
            _channel.QueueBind(queue: queueName, exchange: exchangeName, routingKey: routingKey);

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                if (messageHandler(message))
                {
                    _channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                }
                else
                {
                    _channel.BasicReject(deliveryTag: ea.DeliveryTag, requeue: true);
                }
            };

            _channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);
        }

        public void ReceiveTopicExchangeMessages(string exchangeName, string queueName, string routingKey, Func<string, bool> messageHandler)
        {
            _channel.ExchangeDeclare(exchange: exchangeName, type: ExchangeType.Topic, durable:true);
            _channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
            _channel.QueueBind(queue: queueName, exchange: exchangeName, routingKey: routingKey);

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                if (messageHandler(message))
                {
                    _channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                }
                else
                {
                    _channel.BasicReject(deliveryTag: ea.DeliveryTag, requeue: true);
                }
            };

            _channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);
        }

        public void ReceiveFanoutExchangeMessages(string exchangeName, string queueName, Func<string, bool> messageHandler)
        {
            _channel.ExchangeDeclare(exchange: exchangeName, type: ExchangeType.Fanout, durable:true);
            _channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
            _channel.QueueBind(queue: queueName, exchange: exchangeName, routingKey: "");

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                if (messageHandler(message))
                {
                    _channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                }
                else
                {
                    _channel.BasicReject(deliveryTag: ea.DeliveryTag, requeue: true);
                }
            };

            _channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);
        }

        public void ReceiveHeadersExchangeMessages(string exchangeName, string queueName, IDictionary<string, object> headers, Func<string, bool> messageHandler)
        {
            _channel.ExchangeDeclare(exchange: exchangeName, type: ExchangeType.Headers, durable:true);
            _channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
            _channel.QueueBind(queue: queueName, exchange: exchangeName, routingKey: "", arguments: headers);

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                if (messageHandler(message))
                {
                    _channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                }
                else
                {
                    _channel.BasicReject(deliveryTag: ea.DeliveryTag, requeue: true);
                }
            };

            _channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);
        }

        public void CloseConnection()
        {
            _channel.Close();
            _connection.Close();
        }
    }
}
