using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web_Api_CRUD.Model;
using Web_Api_CRUD.Model.DTO;
using Web_Api_CRUD.Repository;

namespace Web_Api_CRUD.Services
{
    public interface IProdutoService
    {
        Task<Produto> CriarProdutoAsync(ProdutoDTO produtoDto);
        Task<List<Produto>> ObterProdutosPaginadosAsync(int? index = 1, int? size = 10, string? nome = null, decimal? valorMinimo = null, decimal? valorMaximo = null, string? id = null);
        Task<Produto> ObterProdutoPorIdAsync(Guid id);
        Task<Produto> AtualizarProdutoAsync(Guid id, ProdutoDTO produtoDto);
        Task ExcluirProdutoAsync(Guid id);
    }
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _IProdutoRepository;
        public ProdutoService(IProdutoRepository iProdutoRepository)
        {
            _IProdutoRepository = iProdutoRepository;
        }

        public async Task<Produto> CriarProdutoAsync(ProdutoDTO produtoDto)
        {
            return await _IProdutoRepository.CreateAsync(produtoDto);
        }
        public async Task<List<Produto>> ObterProdutosPaginadosAsync(int? index = 1, int? size = 10, string? nome = null, decimal? valorMinimo = null, decimal? valorMaximo = null, string? id = null)
        {
            return await _IProdutoRepository.GetAllPageAsync(index, size, nome, valorMinimo, valorMaximo, id);
        }

        public async Task<Produto> ObterProdutoPorIdAsync(Guid id)
        {
            return await _IProdutoRepository.GetProdutoByIdAsync(id);
        }

        public async Task<Produto> AtualizarProdutoAsync(Guid id, ProdutoDTO produtoDto)
        {
            return await _IProdutoRepository.UpdateAsync(id, produtoDto);
        }

        public async Task ExcluirProdutoAsync(Guid id)
        {
            await _IProdutoRepository.DeleteAsync(id);
        }
    }
}