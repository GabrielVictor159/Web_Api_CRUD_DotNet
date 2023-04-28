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
        Task<Guid> CriarProdutoAsync(ProdutoDTO produtoDto);
        Task<List<Produto>> GetProdutosByIdsAsync(List<Guid> ids);
        Task<List<Produto>> ObterProdutosPaginadosAsync(int indice, int tamanhoPagina);
        Task<Produto> ObterProdutoPorIdAsync(Guid id);
        Task AtualizarProdutoAsync(Guid id, ProdutoDTO produtoDto);
        Task ExcluirProdutoAsync(Guid id);
    }
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _IProdutoRepository;
        public ProdutoService(IProdutoRepository iProdutoRepository)
        {
            _IProdutoRepository = iProdutoRepository;
        }

        public async Task<Guid> CriarProdutoAsync(ProdutoDTO produtoDto)
        {
            return await _IProdutoRepository.CreateAsync(produtoDto);
        }
        public async Task<List<Produto>> GetProdutosByIdsAsync(List<Guid> ids)
        {
            return await _IProdutoRepository.GetProdutosByIdsAsync(ids);
        }
        public async Task<List<Produto>> ObterProdutosPaginadosAsync(int indice, int tamanhoPagina)
        {
            return await _IProdutoRepository.GetAllPageAsync(indice, tamanhoPagina);
        }

        public async Task<Produto> ObterProdutoPorIdAsync(Guid id)
        {
            return await _IProdutoRepository.GetProdutoByIdAsync(id);
        }

        public async Task AtualizarProdutoAsync(Guid id, ProdutoDTO produtoDto)
        {
            await _IProdutoRepository.UpdateAsync(id, produtoDto);
        }

        public async Task ExcluirProdutoAsync(Guid id)
        {
            await _IProdutoRepository.DeleteAsync(id);
        }
    }
}