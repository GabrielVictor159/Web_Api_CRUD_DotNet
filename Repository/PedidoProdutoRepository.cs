using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Web_Api_CRUD.Infraestructure;
using Web_Api_CRUD.Model;
using Web_Api_CRUD.Model.DTO;

namespace Web_Api_CRUD.Repository
{
    public interface IPedidoProdutoRepository
    {
        Task<Guid> CreateAsync(PedidoProdutoDTO pedidoProdutoDto);
        Task<List<PedidoProdutoDTO>> GetAllPageAsync(int index, int size);
        Task<PedidoProduto> GetPedidoProdutoByIdAsync(Guid id);
        Task UpdateAsync(Guid id, PedidoProdutoDTO pedidoProdutoDto);
        Task DeleteAsync(Guid id);
    }
    public class PedidoProdutoRepository : IPedidoProdutoRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public PedidoProdutoRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Guid> CreateAsync(PedidoProdutoDTO pedidoProdutoDto)
        {
            PedidoProduto pedidoProduto = _mapper.Map<PedidoProduto>(pedidoProdutoDto);
            _context.pedidoProdutos.Add(pedidoProduto);
            await _context.SaveChangesAsync();
            return pedidoProduto.Id;
        }

        public async Task<List<PedidoProdutoDTO>> GetAllPageAsync(int index, int size)
        {
            List<PedidoProduto> pedidoProdutos = await Task.FromResult(_context.pedidoProdutos
                .Skip((index - 1) * size)
                .Take(size)
                .ToList());

            return pedidoProdutos.Select(pp => new PedidoProdutoDTO
            {
                Quantidade = pp.Quantidade,
                ValorTotalLinha = pp.ValorTotalLinha,
                PedidoId = pp.idPedido,
                ProdutoId = pp.idProduto
            }).ToList();
        }

        public async Task<PedidoProduto> GetPedidoProdutoByIdAsync(Guid id)
        {
            PedidoProduto pedidoProduto = await Task.FromResult(_context.pedidoProdutos.FirstOrDefault(pp => pp.Id == id));
            return pedidoProduto;
        }

        public async Task UpdateAsync(Guid id, PedidoProdutoDTO pedidoProdutoDto)
        {
            var pedidoProduto = await Task.FromResult(_context.pedidoProdutos.FirstOrDefault(pp => pp.Id == id));
            if (pedidoProduto == null)
            {
                throw new Exception($"PedidoProduto com o ID {id} não encontrado");
            }
            pedidoProduto = _mapper.Map<PedidoProduto>(pedidoProdutoDto);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var pedidoProduto = await Task.FromResult(_context.pedidoProdutos.FirstOrDefault(pp => pp.Id == id));

            if (pedidoProduto == null)
            {
                throw new Exception($"PedidoProduto com o ID {id} não encontrado");
            }

            _context.pedidoProdutos.Remove(pedidoProduto);
            await _context.SaveChangesAsync();
        }
    }
}