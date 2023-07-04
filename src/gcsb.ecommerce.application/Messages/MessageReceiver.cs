using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace gcsb.ecommerce.application.Messages
{
    public class MessageReceiver<T>
    {
        private readonly string? _queueName;
        private readonly string? _exchangeName;
        private readonly string? _routingKey;
        private readonly string? _exchangeType;
        private readonly Func<T, bool>? _messageHandler;
        private readonly bool _requeueRejected;

        private IConnection? _connection;
        private IModel? _channel;

        private MessageReceiver(string queueName, string exchangeName, string routingKey, Func<T, bool> messageHandler, string exchangeType, bool requeueRejected)
        {
            _queueName = queueName;
            _exchangeName = exchangeName;
            _routingKey = routingKey;
            _messageHandler = messageHandler;
            _exchangeType = exchangeType;
            _requeueRejected = requeueRejected;
        }

        public static MessageReceiverBuilder Builder()
        {
            return new MessageReceiverBuilder();
        }

        public void StartReceivingMessages()
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

            _channel.QueueDeclare(_queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

            if (!string.IsNullOrEmpty(_exchangeName))
            {
                if (_exchangeType == "headers")
                {
                    _channel.ExchangeDeclare(_exchangeName, ExchangeType.Headers, durable: true, autoDelete: false, arguments: null);
                    var headerArguments = new Dictionary<string, object> { { "x-match", "all" } };
                    _channel.QueueBind(_queueName, _exchangeName, _routingKey, headerArguments);
                }
                else
                {
                    _channel.ExchangeDeclare(_exchangeName, _exchangeType, durable: true, autoDelete: false, arguments: null);
                    _channel.QueueBind(_queueName, _exchangeName, _routingKey);
                }
            }

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, eventArgs) =>
            {
                var body = eventArgs.Body.ToArray();
                var message = DeserializeMessage(body);
                var result = _messageHandler!.Invoke(message!);

                if (result)
                {
                    _channel.BasicAck(eventArgs.DeliveryTag, multiple: false);
                }
                else
                {
                    if (_requeueRejected)
                    {
                        _channel.BasicReject(eventArgs.DeliveryTag, requeue: true);
                    }
                    else
                    {
                        _channel.BasicReject(eventArgs.DeliveryTag, requeue: false);
                    }
                }
            };

            _channel.BasicConsume(_queueName, autoAck: false, consumer);
        }

        private T? DeserializeMessage(byte[] body)
        {
            var json = System.Text.Encoding.UTF8.GetString(body);
            return JsonConvert.DeserializeObject<T>(json);
        }

        public class MessageReceiverBuilder
        {
            private string? _queueName;
            private string? _exchangeName;
            private string? _routingKey;
            private string? _exchangeType;
            private Func<T, bool>? _messageHandler;
            private bool _requeueRejected;

            public MessageReceiverBuilder WithQueueName(string queueName)
            {
                _queueName = queueName;
                return this;
            }

            public MessageReceiverBuilder WithExchangeName(string exchangeName)
            {
                _exchangeName = exchangeName;
                return this;
            }

            public MessageReceiverBuilder WithRoutingKey(string routingKey)
            {
                _routingKey = routingKey;
                return this;
            }

            public MessageReceiverBuilder WithExchangeType(string exchangeType)
            {
                _exchangeType = exchangeType;
                return this;
            }

            public MessageReceiverBuilder WithMessageHandler(Func<T, bool> messageHandler)
            {
                _messageHandler = messageHandler;
                return this;
            }

            public MessageReceiverBuilder WithRequeueRejected(bool requeueRejected)
            {
                _requeueRejected = requeueRejected;
                return this;
            }

            public MessageReceiver<T> Build()
            {

                if (string.IsNullOrEmpty(_exchangeType))
                {
                    _exchangeType = "direct";
                }

                if (_messageHandler == null)
                {
                    throw new ArgumentNullException(nameof(_messageHandler), "Message handler must be provided.");
                }

                return new MessageReceiver<T>(_queueName!, _exchangeName!, _routingKey!, _messageHandler, _exchangeType, _requeueRejected);
            }
        }
    }
    
}
