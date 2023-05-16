using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Domain.DTO;
using Web_Api_CRUD.Domain;
using Web_Api_CRUD.Domain.DTO;
using Web_Api_CRUD.Repository;

namespace Web_Api_CRUD.Services
{
    public interface IPedidoService
    {
        Task<Object> CreateAsync(Guid idCliente, PedidoDTO pedidoDto);
        Task<Object> GetAllPage(PedidoConsultaDTO dto);
        Task<Object> GetPedidoByIdAsync(Guid id);
        Task<Object> UpdatePedidoAsync(PedidoUpdateDTO dto);
        Task<Object> DeletePedidoAsync(Guid id);
    }
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IProdutoRepository _produtoRepository;
        private readonly IClienteRepository _clienteRepository;
        public PedidoService(IPedidoRepository pedidoRepository,
         IProdutoRepository produtoRepository,
         IClienteRepository clienteRepository)
        {
            _pedidoRepository = pedidoRepository;
            _produtoRepository = produtoRepository;
            _clienteRepository = clienteRepository;
        }
        public async Task<Object> CreateAsync(Guid idCliente, PedidoDTO pedidoDto)
        {
            var dtoValidate = new PedidoDTOValidation().Validate(pedidoDto);
            if (!dtoValidate.IsValid)
            {
                return dtoValidate.ToString();
            }
            var cliente = await _clienteRepository.GetClienteByIdAsync(idCliente);
            if (cliente == null)
            {
                return "O cliente não foi encontrado";
            }
            List<Guid> listaProdutosIds = pedidoDto.listaProdutos.Select(p => p.Produto).ToList();
            List<Produto> listaVerificacao = await _produtoRepository.GetProdutosToListIdsAsync(listaProdutosIds);
            if (listaProdutosIds.Count != listaVerificacao.Count)
            {
                return "Existem produtos na lista que não existem.";
            }
            Pedido pedido = await _pedidoRepository.CreateAsync(idCliente, pedidoDto.listaProdutos);
            return pedido;
        }
        public async Task<Object> GetAllPage(PedidoConsultaDTO dto)
        {
            var dtoValidate = new PedidoConsultaDTOValidation().Validate(dto);
            if (!dtoValidate.IsValid)
            {
                return dtoValidate.ToString();
            }
            var pedidos = await _pedidoRepository.GetAllPageAsync(dto);
            foreach (var pedido in pedidos)
            {
                if (pedido.Id != null)
                {
                    pedido.Lista = await _pedidoRepository.GetPedidoProdutos(pedido.Id);
                }
            }
            return pedidos;
        }
        public async Task<Object> GetPedidoByIdAsync(Guid id)
        {
            var pedido = await _pedidoRepository.GetPedidoByIdAsync(id);
            if (pedido == null)
            {
                return "Pedido não foi encontrado.";
            }
            return pedido;
        }
        public async Task<Object> UpdatePedidoAsync(PedidoUpdateDTO dto)
        {
            var dtoValidate = new PedidoUpdateDTOValidation().Validate(dto);
            if (!dtoValidate.IsValid)
            {
                return dtoValidate.ToString();
            }
            List<Guid> listaProdutosIds = dto.Produtos.Select(p => p.Produto).ToList();
            List<Produto> listaVerificacao = await _produtoRepository.GetProdutosToListIdsAsync(listaProdutosIds);
            if (listaProdutosIds.Count != listaVerificacao.Count)
            {
                return "Existem produtos na lista que não existem.";
            }
            var pedido = await _pedidoRepository.UpdatePedidoAsync(dto.Id, dto.Produtos);
            if (pedido == null)
            {
                return "Não foi possivel buscar dados relacionados ao pedido.";
            }
            return pedido;
        }
        public async Task<Object> DeletePedidoAsync(Guid id)
        {
            var pedido = await _pedidoRepository.GetPedidoByIdAsync(id);
            if (pedido == null)
            {
                return "Pedido não encontrado";
            }
            var delete = await _pedidoRepository.DeletePedidoAsync(id);
            if (delete)
            {
                return true;
            }
            else
            {
                return "Não foi possivel deletar o pedido.";
            }
        }
    }
}