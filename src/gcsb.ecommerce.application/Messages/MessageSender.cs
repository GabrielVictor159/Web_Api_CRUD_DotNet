using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace gcsb.ecommerce.application.Messages
{
    public class MessageSender<T>
    {
        private readonly string _exchangeName;
        private readonly string _queueName ="";
        private readonly string _routingKey = "";
        private readonly string _exchangeType = "";
        private readonly IDictionary<string, object>? _headers;

        private IConnection? _connection;
        private IModel? _channel;

        private MessageSender(string exchangeType, IDictionary<string, object>? headers, string exchangeName = "", string queueName = "", string routingKey ="")
        {
            _exchangeName = exchangeName;
            _queueName = queueName;
            _routingKey = routingKey;
            _headers = headers;
            _exchangeType = exchangeType;
        }

        public static MessageSenderBuilder Builder()
        {
            return new MessageSenderBuilder();
        }

        public void SendMessage(T message)
        {
            var factory = new ConnectionFactory
            {
                HostName = Environment.GetEnvironmentVariable("RabbitMQ_HostName") ?? "localhost",
                Port = int.Parse(Environment.GetEnvironmentVariable("RabbitMQ_Port") ?? "5672"),
                UserName = Environment.GetEnvironmentVariable("RabbitMQ_UserName") ?? "guest",
                Password = Environment.GetEnvironmentVariable("RabbitMQ_Password") ?? "guest",
                VirtualHost = Environment.GetEnvironmentVariable("RabbitMQ_VirtualHost") ?? "/"
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            if (!string.IsNullOrEmpty(_exchangeName))
            {
                _channel.ExchangeDeclare(_exchangeName, _exchangeType, durable: true, autoDelete: false, arguments: null); 

                if (!string.IsNullOrEmpty(_queueName))
                {
                    _channel.QueueDeclare(_queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
                    _channel.QueueBind(_queueName, _exchangeName, _routingKey, arguments: null);
                }
            }

            var json = JsonConvert.SerializeObject(message);
            var body = System.Text.Encoding.UTF8.GetBytes(json);

            var properties = _channel.CreateBasicProperties();
            properties.Persistent = true;

            if (_headers != null && _headers.Count > 0)
            {
                properties.Headers = _headers;
            }

            _channel.BasicPublish(_exchangeName, _routingKey, properties, body);

            _channel.Close();
            _connection.Close();
        }

        public class MessageSenderBuilder
        {
            private string _exchangeName = "";
            private string _queueName ="";
            private string _routingKey = "";
            private string _exchangeType = "";
            private IDictionary<string, object>? _headers;

            public MessageSenderBuilder WithQueueName(string queueName)
            {
                _queueName = queueName;
                return this;
            }

            public MessageSenderBuilder WithExchangeName(string exchangeName)
            {
                _exchangeName = exchangeName;
                return this;
            }

            public MessageSenderBuilder WithRoutingKey(string routingKey)
            {
                _routingKey = routingKey;
                return this;
            }

            public MessageSenderBuilder WithHeaders(IDictionary<string, object> headers)
            {
                _headers = headers;
                return this;
            }

            public MessageSenderBuilder WithExchangeType(string exchangeType)
            {
                _exchangeType = exchangeType;
                return this;
            }

            public MessageSender<T> Build()
            {
                return new MessageSender<T>(_exchangeType,_headers, _exchangeName, _queueName, _routingKey);
            }
        }
    }
}
