using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Web_Api_CRUD.Model.DTO;
using Web_Api_CRUD.Repository;

namespace Web_Api_CRUD.Services
{
    public interface IPedidoProdutoService
    {
        Task<Guid> CreateAsync(PedidoProdutoDTO pedidoProdutoDto);
        Task<List<PedidoProdutoDTO>> GetAllPageAsync(int index, int size);
        Task<PedidoProdutoDTO> GetPedidoProdutoByIdAsync(Guid id);
        Task UpdateAsync(Guid id, PedidoProdutoDTO pedidoProdutoDto);
        Task DeleteAsync(Guid id);
    }
    public class PedidoProdutoService : IPedidoProdutoService
    {
        private readonly IPedidoProdutoRepository _pedidoProdutoRepository;
        private readonly IMapper _mapper;

        public PedidoProdutoService(IPedidoProdutoRepository pedidoProdutoRepository, IMapper mapper)
        {
            _pedidoProdutoRepository = pedidoProdutoRepository;
            _mapper = mapper;
        }

        public async Task<Guid> CreateAsync(PedidoProdutoDTO pedidoProdutoDto)
        {
            var pedidoProdutoId = await _pedidoProdutoRepository.CreateAsync(pedidoProdutoDto);
            return pedidoProdutoId;
        }

        public async Task<List<PedidoProdutoDTO>> GetAllPageAsync(int index, int size)
        {
            var pedidoProdutos = await _pedidoProdutoRepository.GetAllPageAsync(index, size);
            var pedidoProdutosDto = pedidoProdutos.Select(pp => _mapper.Map<PedidoProdutoDTO>(pp)).ToList();
            return pedidoProdutosDto;
        }

        public async Task<PedidoProdutoDTO> GetPedidoProdutoByIdAsync(Guid id)
        {
            var pedidoProduto = await _pedidoProdutoRepository.GetPedidoProdutoByIdAsync(id);
            var pedidoProdutoDto = _mapper.Map<PedidoProdutoDTO>(pedidoProduto);
            return pedidoProdutoDto;
        }

        public async Task UpdateAsync(Guid id, PedidoProdutoDTO pedidoProdutoDto)
        {
            await _pedidoProdutoRepository.UpdateAsync(id, pedidoProdutoDto);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _pedidoProdutoRepository.DeleteAsync(id);
        }
    }


}