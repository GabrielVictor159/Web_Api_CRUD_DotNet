using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Domain.DTO;
using API.Infraestructure;
using AutoMapper;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Web_Api_CRUD.Domain;
using Web_Api_CRUD.Domain.DTO;
using Web_Api_CRUD.Repository;
using Web_Api_CRUD.Services;

namespace API.Application
{
    public class ApiBackgroundService : BackgroundService
    {
        private readonly IMessagingQeue _messagingQeue;
        private readonly int _timeDelay = 5;
        private readonly IPedidoService _pedidoService;
        private readonly IMapper _mapper;
        private readonly IPedidoRepository _pedidoRepository;
        public ApiBackgroundService(IConfiguration configuration, IMessagingQeue messagingQeue, IPedidoService pedidoService, IMapper mapper, IPedidoRepository pedidoRepository)
        {
            _messagingQeue = messagingQeue;
            var timeDelay = configuration.GetValue<int?>("MessagingQeueDelaySecondsBackgroundSize");
            if (timeDelay != null)
            {
                _timeDelay = (int)timeDelay;
            }
            _pedidoService = pedidoService;
            _mapper = mapper;
            _pedidoRepository = pedidoRepository;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var message = await _messagingQeue.ReceiveDefaultQueue("Pagamentos", async (message, channel, model, args) =>
                 {
                     Console.WriteLine(message);
                     PaymentMessageDTO payment = JsonConvert.DeserializeObject<PaymentMessageDTO>(message) ?? new PaymentMessageDTO();
                     var pedido = await _pedidoService.GetPedidoByIdAsync(payment.IdPedido);
                     if (pedido is Pedido p)
                     {
                         PedidoUpdateDTO updateDTO = _mapper.Map<PedidoUpdateDTO>(p);
                         updateDTO.IdPagamento = payment.IdPayment;
                         p.Lista = await _pedidoRepository.GetPedidoProdutos(p.Id);
                         List<ProdutoQuantidadeDTO> listQuantidade = new();
                         foreach (PedidoProduto pedidoProduto in p.Lista)
                         {
                             listQuantidade.Add(new ProdutoQuantidadeDTO() { Produto = pedidoProduto.IdProduto, Quantidade = pedidoProduto.Quantidade });
                         }
                         updateDTO.listaProdutos = listQuantidade;
                         var updateResult = await _pedidoService.UpdatePedidoAsync(updateDTO);
                         if (updateResult is Pedido)
                         {
                             if (channel != null && args != null)
                             {
                                 channel.BasicAck(args.DeliveryTag, multiple: false);
                             }
                         }
                         else
                         {
                             if (channel != null && args != null)
                             {
                                 channel.BasicReject(args.DeliveryTag, requeue: true);
                             }
                         }
                     }
                     else
                     {
                         if (channel != null && args != null)
                         {
                             channel.BasicReject(args.DeliveryTag, requeue: true);
                         }
                     }
                 });
                await Task.Delay(TimeSpan.FromSeconds(_timeDelay), stoppingToken);
            }
        }
    }
}