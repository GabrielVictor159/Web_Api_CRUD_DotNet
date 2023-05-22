using System;
using System.Threading.Tasks;
using API.Infraestructure;
using Moq;
using static API.Infraestructure.MessagingQeue;

namespace TEST.Infraestructure
{
    public class MessagingQeueMock : IMessagingQeue
    {
        private readonly Mock<IMessagingQeue> _mock;

        public MessagingQeueMock()
        {
            _mock = new Mock<IMessagingQeue>();

            ConfigureMock();
        }

        public IMessagingQeue Object => _mock.Object;

        private void ConfigureMock()
        {
            _mock.Setup(m => m.SendDirectExchange(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>()))
                .Verifiable();

            _mock.Setup(m => m.SendTopicExchange(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>()))
                .Verifiable();

            _mock.Setup(m => m.SendFanoutExchange(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>()))
                .Verifiable();

            _mock.Setup(m => m.SendDefaultQueue(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>()))
                .Verifiable();

            _mock.Setup(m => m.ReceiveDirectExchange(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<HandleMessage>()))
                 .Returns((string exchangeName, string queueName, string routingKey, HandleMessage handleMessage) =>
                 {
                     return Task.Run(async () =>
                     {
                         await handleMessage.Invoke("Mensagem simulada", null, null, null);
                         return (string?)null;
                     });
                 });

            _mock.Setup(m => m.ReceiveTopicExchange(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<HandleMessage>()))
                .Returns((string exchangeName, string queueName, string routingKeyPattern, HandleMessage handleMessage) =>
                {
                    return Task.Run(async () =>
                    {
                        await handleMessage.Invoke("Mensagem simulada", null, null, null);
                        return (string?)null;
                    });
                });

            _mock.Setup(m => m.ReceiveFanoutExchange(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<HandleMessage>()))
                .Returns((string exchangeName, string queueName, HandleMessage handleMessage) =>
                {
                    return Task.Run(async () =>
                    {
                        await handleMessage.Invoke("Mensagem simulada", null, null, null);
                        return (string?)null;
                    });
                });

            _mock.Setup(m => m.ReceiveDefaultQueue(It.IsAny<string>(), It.IsAny<HandleMessage>()))
                .Returns((string queueName, HandleMessage handleMessage) =>
                {
                    return Task.Run(async () =>
                    {
                        await handleMessage.Invoke("Mensagem simulada", null, null, null);
                        return (string?)null;
                    });
                });
        }

        public void SendDirectExchange(string exchangeName, string routingKey, string message, bool persistent = false)
        {
            _mock.Object.SendDirectExchange(exchangeName, routingKey, message, persistent);
        }

        public void SendTopicExchange(string exchangeName, string routingKey, string message, bool persistent = false)
        {
            _mock.Object.SendTopicExchange(exchangeName, routingKey, message, persistent);
        }

        public void SendFanoutExchange(string exchangeName, string message, bool persistent = false)
        {
            _mock.Object.SendFanoutExchange(exchangeName, message, persistent);
        }

        public void SendDefaultQueue(string queueName, string message, bool persistent = false)
        {
            _mock.Object.SendDefaultQueue(queueName, message, persistent);
        }


        public Task<string?> ReceiveDirectExchange(string exchangeName, string queueName, string routingKey, HandleMessage handleMessage)
        {
            return _mock.Object.ReceiveDirectExchange(exchangeName, queueName, routingKey, handleMessage);
        }

        public Task<string?> ReceiveTopicExchange(string exchangeName, string queueName, string routingKeyPattern, HandleMessage handleMessage)
        {
            return _mock.Object.ReceiveTopicExchange(exchangeName, queueName, routingKeyPattern, handleMessage);
        }

        public Task<string?> ReceiveFanoutExchange(string exchangeName, string queueName, HandleMessage handleMessage)
        {
            return _mock.Object.ReceiveFanoutExchange(exchangeName, queueName, handleMessage);
        }

        public Task<string?> ReceiveDefaultQueue(string queueName, HandleMessage handleMessage)
        {
            return _mock.Object.ReceiveDefaultQueue(queueName, handleMessage);
        }
    }
}
