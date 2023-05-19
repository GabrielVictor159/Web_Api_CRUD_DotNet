using System;
using System.Threading.Tasks;
using API.Infraestructure;
using Moq;

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

            _mock.Setup(m => m.ReceiveDirectExchange(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Action<string>>()))
                 .Returns((string exchangeName, string queueName, string routingKey, Action<string> handleMessage) =>
                {
                    handleMessage.Invoke("Mensagem simulada");
                    return Task.FromResult<string?>(null);
                });
            _mock.Setup(m => m.ReceiveTopicExchange(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Action<string>>()))
                .Returns((string exchangeName, string queueName, string routingKeyPattern, Action<string> handleMessage) =>
                {
                    handleMessage.Invoke("Mensagem simulada");
                    return Task.FromResult<string?>(null);
                });

            _mock.Setup(m => m.ReceiveFanoutExchange(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Action<string>>()))
                .Returns((string exchangeName, string queueName, Action<string> handleMessage) =>
                {
                    handleMessage.Invoke("Mensagem simulada");
                    return Task.FromResult<string?>(null);
                });

            _mock.Setup(m => m.ReceiveDefaultQueue(It.IsAny<string>(), It.IsAny<Action<string>>()))
                .Returns((string queueName, Action<string> handleMessage) =>
                {
                    handleMessage.Invoke("Mensagem simulada");
                    return Task.FromResult<string?>(null);
                });
        }
        public void SendDirectExchange(string exchangeName, string queueName, string routingKey, bool durable)
        {
            _mock.Object.SendDirectExchange(exchangeName, queueName, routingKey, durable);
        }

        public void SendTopicExchange(string exchangeName, string queueName, string routingKeyPattern, bool durable)
        {
            _mock.Object.SendTopicExchange(exchangeName, queueName, routingKeyPattern, durable);
        }

        public void SendFanoutExchange(string exchangeName, string queueName, bool durable)
        {
            _mock.Object.SendFanoutExchange(exchangeName, queueName, durable);
        }

        public void SendDefaultQueue(string queueName, string message, bool durable)
        {
            _mock.Object.SendDefaultQueue(queueName, message, durable);
        }

        public Task<string?> ReceiveDirectExchange(string exchangeName, string queueName, string routingKey, Action<string> handleMessage)
        {
            return _mock.Object.ReceiveDirectExchange(exchangeName, queueName, routingKey, handleMessage);
        }

        public Task<string?> ReceiveTopicExchange(string exchangeName, string queueName, string routingKeyPattern, Action<string> handleMessage)
        {
            return _mock.Object.ReceiveTopicExchange(exchangeName, queueName, routingKeyPattern, handleMessage);
        }

        public Task<string?> ReceiveFanoutExchange(string exchangeName, string queueName, Action<string> handleMessage)
        {
            return _mock.Object.ReceiveFanoutExchange(exchangeName, queueName, handleMessage);
        }

        public Task<string?> ReceiveDefaultQueue(string queueName, Action<string> handleMessage)
        {
            return _mock.Object.ReceiveDefaultQueue(queueName, handleMessage);
        }
    }
}
