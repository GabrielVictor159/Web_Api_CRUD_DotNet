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
            _mock.Setup(m => m.SendDefaultQueue(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>()))
                .Verifiable();

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

        public void SendDefaultQueue(string queueName, string message, bool persistent = false)
        {
            _mock.Object.SendDefaultQueue(queueName, message, persistent);
        }

        public Task<string?> ReceiveDefaultQueue(string queueName, HandleMessage handleMessage)
        {
            return _mock.Object.ReceiveDefaultQueue(queueName, handleMessage);
        }
    }
}
