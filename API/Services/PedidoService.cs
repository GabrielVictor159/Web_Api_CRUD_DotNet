using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web_Api_CRUD.Domain;
using Web_Api_CRUD.Domain.DTO;
using Web_Api_CRUD.Repository;

namespace Web_Api_CRUD.Services
{
    public interface IPedidoService
    {
        Task<Pedido> CriarPedidoAsync(Guid id, List<ProdutoQuantidadeDTO> pedidoDto);
        Task<List<Pedido>> GetAllPage(PedidoConsultaDTO dto);
        Task<Pedido> GetPedidoByIdAsync(Guid id);
        Task<Pedido> UpdatePedidoAsync(PedidoUpdateDTO dto);
        Task DeletePedidoAsync(Guid id);
    }
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepository _IPedidoRepository;
        public PedidoService(IPedidoRepository pedidoRepository)
        {
            _IPedidoRepository = pedidoRepository;
        }
        public async Task<Pedido> CriarPedidoAsync(Guid id, List<ProdutoQuantidadeDTO> pedidoDto)
        {
            return await _IPedidoRepository.CreateAsync(id, pedidoDto);
        }
        public async Task<List<Pedido>> GetAllPage(PedidoConsultaDTO dto)
        {
            return await _IPedidoRepository.GetAllPageAsync(dto);
        }
        public async Task<Pedido> GetPedidoByIdAsync(Guid id)
        {
            return await _IPedidoRepository.GetPedidoByIdAsync(id);
        }
        public async Task<Pedido> UpdatePedidoAsync(PedidoUpdateDTO dto)
        {
            return await _IPedidoRepository.UpdatePedidoAsync(dto.Id, dto.Produtos);
        }
        public async Task DeletePedidoAsync(Guid id)
        {
            await _IPedidoRepository.DeletePedidoAsync(id);
        }
    }
}