using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public PedidoProdutoRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Guid> CreateAsync(PedidoProdutoDTO pedidoProdutoDto)
        {
            var pedidoProduto = new PedidoProduto
            {
                Quantidade = pedidoProdutoDto.Quantidade,
                ValorTotalLinha = pedidoProdutoDto.ValorTotalLinha,
                idPedido = pedidoProdutoDto.PedidoId,
                idProduto = pedidoProdutoDto.ProdutoId
            };

            _context.pedidoProdutos.Add(pedidoProduto);
            await _context.SaveChangesAsync();

            return pedidoProduto.Id;
        }

        public async Task<List<PedidoProdutoDTO>> GetAllPageAsync(int index, int size)
        {
            var pedidoProdutos = await Task.FromResult(_context.pedidoProdutos
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
            var pedidoProduto = await Task.FromResult(_context.pedidoProdutos.FirstOrDefault(pp => pp.Id == id));
            return pedidoProduto;
        }

        public async Task UpdateAsync(Guid id, PedidoProdutoDTO pedidoProdutoDto)
        {
            var pedidoProduto = await Task.FromResult(_context.pedidoProdutos.FirstOrDefault(pp => pp.Id == id));

            if (pedidoProduto == null)
            {
                throw new Exception($"PedidoProduto com o ID {id} não encontrado");
            }

            pedidoProduto.Quantidade = pedidoProdutoDto.Quantidade;
            pedidoProduto.ValorTotalLinha = pedidoProdutoDto.ValorTotalLinha;
            pedidoProduto.idPedido = pedidoProdutoDto.PedidoId;
            pedidoProduto.idProduto = pedidoProdutoDto.ProdutoId;

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