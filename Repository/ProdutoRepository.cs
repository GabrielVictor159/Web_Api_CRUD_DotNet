using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Web_Api_CRUD.Infraestructure;
using Web_Api_CRUD.Model;
using Web_Api_CRUD.Model.DTO;

namespace Web_Api_CRUD.Repository
{
    public interface IProdutoRepository
    {
        Task<Guid> CreateAsync(ProdutoDTO produtoDto);
        Task<List<Produto>> GetAllPageAsync(int index, int size);
        Task<Produto> GetProdutoByIdAsync(Guid id);
        Task<List<Produto>> GetProdutosByIdsAsync(List<Guid> ids);
        Task UpdateAsync(Guid id, ProdutoDTO produtoDto);
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
        public async Task<Guid> CreateAsync(ProdutoDTO produtoDto)
        {
            Produto produto = _mapper.Map<Produto>(produtoDto);
            _context.produtos.Add(produto);
            await _context.SaveChangesAsync();
            return produto.Id;
        }

        public async Task<List<Produto>> GetAllPageAsync(int index, int size)
        {
            List<Produto> produtos = await Task.FromResult(_context.produtos
                .Skip((index - 1) * size)
                .Take(size)
                .ToList());

            return produtos;
        }

        public async Task<Produto> GetProdutoByIdAsync(Guid id)
        {
            var produto = await Task.FromResult(_context.produtos.FirstOrDefault(p => p.Id == id));
            return produto;
        }
        public async Task<List<Produto>> GetProdutosByIdsAsync(List<Guid> ids)
        {
            var produtos = await Task.FromResult(_context.produtos.Where(p => ids.Contains(p.Id)).ToList());
            return produtos;
        }
        public async Task UpdateAsync(Guid id, ProdutoDTO produtoDto)
        {
            var produto = await Task.FromResult(_context.produtos.FirstOrDefault(p => p.Id == id));
            if (produto == null)
            {
                throw new Exception($"Produto com o ID {id} não encontrado");
            }
            produto = _mapper.Map<Produto>(produtoDto);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var produto = await _context.produtos.Include(p => p.pedidoProduto)
                                                 .FirstOrDefaultAsync(p => p.Id == id);
            if (produto == null)
            {
                throw new Exception($"Produto com o ID {id} não encontrado");
            }

            _context.pedidoProdutos.RemoveRange(produto.pedidoProduto);
            _context.produtos.Remove(produto);
            await _context.SaveChangesAsync();
        }
    }
}