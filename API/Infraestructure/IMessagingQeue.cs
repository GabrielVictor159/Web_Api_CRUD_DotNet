using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Infraestructure
{
    public interface IMessagingQeue
    {
        void SendDirectExchange(string exchangeName, string routingKey, string message, Boolean persistent = false);
        void SendTopicExchange(string exchangeName, string routingKey, string message, Boolean persistent = false);
        void SendFanoutExchange(string exchangeName, string message, Boolean persistent = false);
        void SendDefaultQueue(string queueName, string message, Boolean persistent = false);
        Task<string?> ReceiveDirectExchange(string exchangeName, string queueName, string routingKey, Action<string> handleMessage);
        Task<string?> ReceiveTopicExchange(string exchangeName, string queueName, string routingKeyPattern, Action<string> handleMessage);
        Task<string?> ReceiveFanoutExchange(string exchangeName, string queueName, Action<string> handleMessage);
        Task<string?> ReceiveDefaultQueue(string queueName, Action<string> handleMessage);
    }
}