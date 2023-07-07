using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gcsb.ecommerce.application.Interfaces.Messages
{
    public interface IMessageReceiver
{
    void ReceiveDirectExchangeMessages(string exchangeName, string queueName, string routingKey, Func<string, bool> messageHandler);
    void ReceiveTopicExchangeMessages(string exchangeName, string queueName, string routingKey, Func<string, bool> messageHandler);
    void ReceiveFanoutExchangeMessages(string exchangeName, string queueName, Func<string, bool> messageHandler);
    void ReceiveHeadersExchangeMessages(string exchangeName, string queueName, IDictionary<string, object> headers, Func<string, bool> messageHandler);
    void CloseConnection();
}

}