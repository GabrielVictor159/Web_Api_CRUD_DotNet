using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Infraestructure;

namespace API.Application
{
    public class ApiBackgroundService : BackgroundService
    {
        private readonly IMessagingQeue _messagingQeue;
        private readonly int _timeDelay = 5;
        public ApiBackgroundService(IConfiguration configuration, IMessagingQeue messagingQeue)
        {
            _messagingQeue = messagingQeue;
            var timeDelay = configuration.GetValue<int?>("MessagingQeueDelaySecondsBackgroundSize");
            if (timeDelay != null)
            {
                _timeDelay = (int)timeDelay;
            }
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var message = await _messagingQeue.ReceiveFanoutExchange("Pagamentos", "ApiPedidos", HandlePayment);
                await Task.Delay(TimeSpan.FromSeconds(_timeDelay), stoppingToken);
            }
        }

        private void HandlePayment(string message)
        {
            Console.WriteLine(message);
        }
    }
}