using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web_Api_CRUD.Infraestructure;
using Web_Api_CRUD.Model;
using Web_Api_CRUD.Model.DTO;

namespace Web_Api_CRUD.Repository
{
    public interface IProdutoRepository
    {
        Task<Guid> CreateAsync(ProdutoDTO produtoDto);
        Task<List<ProdutoDTO>> GetAllPageAsync(int index, int size);
        Task<Produto> GetProdutoByIdAsync(Guid id);
        Task UpdateAsync(Guid id, ProdutoDTO produtoDto);
        Task DeleteAsync(Guid id);
    }
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly ApplicationDbContext _context;

        public ProdutoRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Guid> CreateAsync(ProdutoDTO produtoDto)
        {
            var produto = new Produto
            {
                Nome = produtoDto.Nome,
                Valor = produtoDto.Valor
            };

            _context.produtos.Add(produto);
            await _context.SaveChangesAsync();

            return produto.Id;
        }

        public async Task<List<ProdutoDTO>> GetAllPageAsync(int index, int size)
        {
            var produtos = await Task.FromResult(_context.produtos
                .Skip((index - 1) * size)
                .Take(size)
                .ToList());

            return produtos.Select(p => new ProdutoDTO { Nome = p.Nome, Valor = p.Valor }).ToList();
        }

        public async Task<Produto> GetProdutoByIdAsync(Guid id)
        {
            var produto = await Task.FromResult(_context.produtos.FirstOrDefault(p => p.Id == id));
            return produto;
        }

        public async Task UpdateAsync(Guid id, ProdutoDTO produtoDto)
        {
            var produto = await Task.FromResult(_context.produtos.FirstOrDefault(p => p.Id == id));

            if (produto == null)
            {
                throw new Exception($"Produto com o ID {id} não encontrado");
            }

            produto.Nome = produtoDto.Nome;
            produto.Valor = produtoDto.Valor;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var produto = await Task.FromResult(_context.produtos.FirstOrDefault(p => p.Id == id));

            if (produto == null)
            {
                throw new Exception($"Produto com o ID {id} não encontrado");
            }

            _context.produtos.Remove(produto);
            await _context.SaveChangesAsync();
        }
    }
}