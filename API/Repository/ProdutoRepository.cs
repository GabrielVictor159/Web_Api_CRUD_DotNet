using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Web_Api_CRUD.Exceptions;
using Web_Api_CRUD.Infraestructure;
using Web_Api_CRUD.Model;
using Web_Api_CRUD.Model.DTO;

namespace Web_Api_CRUD.Repository
{
    public interface IProdutoRepository
    {
        Task<Produto> CreateAsync(ProdutoDTO produtoDto);
        Task<List<Produto>> GetAllPageAsync(int? index = 1, int? size = 10, string? nome = null, decimal? valorMinimo = null, decimal? valorMaximo = null, string? id = null);
        Task<Produto> GetProdutoByIdAsync(Guid id);
        Task<Produto> UpdateAsync(Guid id, ProdutoDTO produtoDto);
        Task DeleteAsync(Guid id);
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
            if (await _context.produtos.AnyAsync(c => c.Nome == produtoDto.Nome))
            {
                throw new ProdutoRegisterException("Já existe um produto com o mesmo nome.");
            }
            Produto produto = _mapper.Map<Produto>(produtoDto);
            _context.produtos.Add(produto);
            await _context.SaveChangesAsync();
            return produto;
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

            foreach (var produto in produtos)
            {
                produto.Lista = await GetPedidoProdutos(produto.Id);
            }
            return produtos;
        }
        public async Task<Produto> GetProdutoByIdAsync(Guid id)
        {
            var produto = await Task.FromResult(_context.produtos.FirstOrDefault(p => p.Id == id));
            if (produto == null)
            {
                throw new ProdutoConsultaException($"Produto comm o ID: {id} não encontrado");
            }
            return produto;
        }
        public async Task<Produto> UpdateAsync(Guid id, ProdutoDTO produtoDto)
        {
            var produto = await _context.produtos.FirstOrDefaultAsync(p => p.Id == id);
            if (produto == null)
            {
                throw new ProdutoConsultaException($"Produto com o ID {id} não encontrado");
            }

            _mapper.Map(produtoDto, produto);
            await _context.SaveChangesAsync();
            return produto;
        }


        public async Task DeleteAsync(Guid id)
        {
            var produto = await _context.produtos.FindAsync(id);
            if (produto == null)
            {
                throw new ProdutoConsultaException($"Produto com o ID: {id} não encontrado");
            }

            _context.produtos.Remove(produto);
            await _context.SaveChangesAsync();
        }
        private async Task<List<PedidoProduto>> GetPedidoProdutos(Guid idProduto)
        {
            List<PedidoProduto> pedidosProdutos = await _context.pedidoProdutos
                .Include(pp => pp.Pedido)
                .Where(pp => pp.IdProduto == idProduto)
                .ToListAsync();
            return pedidosProdutos;
        }
    }
}