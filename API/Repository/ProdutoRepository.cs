using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Web_Api_CRUD.Exceptions;
using Web_Api_CRUD.Infraestructure;
using Web_Api_CRUD.Domain;
using Web_Api_CRUD.Domain.DTO;

namespace Web_Api_CRUD.Repository
{
    public interface IProdutoRepository
    {
        Task<Produto> CreateAsync(ProdutoDTO produtoDto);
        Task<Produto?> GetProdutoByNameAsync(String Nome);
        Task<List<Produto>> GetAllPageAsync(int? index = 1, int? size = 10, string? nome = null, decimal? valorMinimo = null, decimal? valorMaximo = null, string? id = null);
        Task<Produto?> GetProdutoByIdAsync(Guid id);
        Task<Produto?> UpdateAsync(Guid id, ProdutoDTO produtoDto);
        Task<Boolean> DeleteAsync(Guid id);
        Task<List<PedidoProduto>> GetPedidoProdutos(Guid idProduto);
        Task<List<Produto>> GetProdutosToListIdsAsync(List<Guid> listIds);
    }
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public ProdutoRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Produto> CreateAsync(ProdutoDTO produtoDto)
        {
            Produto produto = _mapper.Map<Produto>(produtoDto);
            _context.produtos.Add(produto);
            await _context.SaveChangesAsync();
            return produto;
        }
        public async Task<Produto?> GetProdutoByNameAsync(String Nome)
        {
            return await Task.FromResult(_context.produtos.FirstOrDefault(p => p.Nome == Nome));
        }
        public async Task<List<Produto>> GetAllPageAsync(int? index = 1, int? size = 10, string? nome = null, decimal? valorMinimo = null, decimal? valorMaximo = null, string? id = null)
        {
            var query = _context.produtos.AsQueryable();

            if (!string.IsNullOrEmpty(nome))
            {
                query = query.Where(p => p.Nome.ToLower().Contains(nome.ToLower()));
            }

            if (valorMinimo.HasValue)
            {
                query = query.Where(p => p.Valor >= valorMinimo.Value);
            }

            if (valorMaximo.HasValue)
            {
                query = query.Where(p => p.Valor <= valorMaximo.Value);
            }

            if (!string.IsNullOrEmpty(id))
            {
                query = query.Where(p => p.Id.ToString().ToLower().Contains(id.ToLower()));
            }

            var produtos = await query.Skip((int)((index - 1) * size))
                                      .Take((int)size)
                                      .ToListAsync();

            return produtos;
        }
        public async Task<Produto?> GetProdutoByIdAsync(Guid id)
        {
            return await Task.FromResult(_context.produtos.FirstOrDefault(p => p.Id == id));
        }
        public async Task<Produto?> UpdateAsync(Guid id, ProdutoDTO produtoDto)
        {
            var produto = await _context.produtos.FirstOrDefaultAsync(p => p.Id == id);
            if (produto == null)
            {
                return null;
            }
            _mapper.Map(produtoDto, produto);
            await _context.SaveChangesAsync();
            return produto;
        }


        public async Task<Boolean> DeleteAsync(Guid id)
        {
            var produto = await _context.produtos.FindAsync(id);
            if (produto != null)
            {
                _context.produtos.Remove(produto);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<List<PedidoProduto>> GetPedidoProdutos(Guid idProduto)
        {
            List<PedidoProduto> pedidosProdutos = await _context.pedidoProdutos
                .Include(pp => pp.Pedido)
                .Where(pp => pp.IdProduto == idProduto)
                .ToListAsync();
            return pedidosProdutos;
        }
        public async Task<List<Produto>> GetProdutosToListIdsAsync(List<Guid> listIds)
        {
            return await _context.produtos.Where(p => listIds.Contains(p.Id)).ToListAsync();
        }


    }
}