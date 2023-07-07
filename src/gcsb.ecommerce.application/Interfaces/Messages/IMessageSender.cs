using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gcsb.ecommerce.application.Interfaces.Messages
{
    public interface IMessageSender
{
    void SendDirectExchangeMessage(string exchangeName, string routingKey, string message);
    void SendTopicExchangeMessage(string exchangeName, string routingKey, string message);
    void SendFanoutExchangeMessage(string exchangeName, string message);
    void SendHeadersExchangeMessage(string exchangeName, string message, IDictionary<string, object> headers);
    void CloseConnection();
    
}

}