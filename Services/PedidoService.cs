using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web_Api_CRUD.Model;
using Web_Api_CRUD.Model.DTO;
using Web_Api_CRUD.Repository;

namespace Web_Api_CRUD.Services
{
    public interface IPedidoService
    {
        Task<Pedido> CriarPedidoAsync(PedidoDTO pedidoDto);
        Task<List<Pedido>> ObterPedidosPaginadosAsync(int indice, int tamanhoPagina);
        Task<Pedido> ObterPedidoPorIdAsync(Guid id);
        Task AtualizarPedidoAsync(Guid id, PedidoDTO pedidoDto);
        Task ExcluirPedidoAsync(Guid id);
    }
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepository _IPedidoRepository;
        public PedidoService(IPedidoRepository pedidoRepository)
        {
            _IPedidoRepository = pedidoRepository;
        }
        public async Task<Pedido> CriarPedidoAsync(PedidoDTO pedidoDto)
        {
            return await _IPedidoRepository.CreateAsync(pedidoDto);
        }

        public async Task<List<Pedido>> ObterPedidosPaginadosAsync(int indice, int tamanhoPagina)
        {
            return await _IPedidoRepository.GetAllPageAsync(indice, tamanhoPagina);
        }

        public async Task<Pedido> ObterPedidoPorIdAsync(Guid id)
        {
            return await _IPedidoRepository.GetPedidoByIdAsync(id);
        }

        public async Task AtualizarPedidoAsync(Guid id, PedidoDTO pedidoDto)
        {
            await _IPedidoRepository.UpdateAsync(id, pedidoDto);
        }

        public async Task ExcluirPedidoAsync(Guid id)
        {
            await _IPedidoRepository.DeleteAsync(id);
        }


    }
}